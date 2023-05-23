using ErrorOr;
using Inspiration.Contract;
using Inspiration.Domain.Services.Interfaces;
using Inspiration.Infrastructure.FileStorage;
using Inspiration.Repository.DataAccessObjects;
using Inspiration.Repository.Interfaces;

namespace Inspiration.Domain.Services;

public class NPUService : INPUService
{
    private readonly INPURepository _nPURepository;
    private readonly IUserRepository _userRepository;
    private readonly IPartRepository _partRepository;
    private readonly ICreativityRatingRepository _creativityRatingRepository;
    private readonly IUniquenessRatingRepository _uniquenessRatingRepository;
    private readonly IFileStorage _fileStorage;
    public NPUService(INPURepository nPURepository, IUserRepository userRepository, IPartRepository partRepository, IFileStorage fileStorage, ICreativityRatingRepository creativityRatingRepository, IUniquenessRatingRepository uniquenessRatingRepository)
    {
        _nPURepository = nPURepository;
        _userRepository = userRepository;
        _partRepository = partRepository;
        _fileStorage = fileStorage;
        _creativityRatingRepository = creativityRatingRepository;
        _uniquenessRatingRepository = uniquenessRatingRepository;
    }

    public async Task<ErrorOr<NPUResponseDto>> CreateAsync(NPUCreateRequestDto npu, Guid userId, MemoryStream ms, string fileExtension, CancellationToken cancellationToken = default)
    {
        //DTO validation
        var dtoValidation = ValidateNPUCreateRequestDto(npu);
        if (dtoValidation.Count > 0)
        {
            return dtoValidation;
        }

        var user = _userRepository.Get(userId);

        if (user == null)
        {
            return Errors.User.UserNotFound;
        }

        var imageCode = await _fileStorage.SaveAsync(ms, fileExtension, cancellationToken);
        if (string.IsNullOrEmpty(imageCode))
        {
            return Errors.NPU.CouldNotSaveImage;
        }

        var dao = new NPUDao(Guid.NewGuid(), user.Id, npu.Description, imageCode, false, DateTime.UtcNow, DateTime.UtcNow);
        //TODO: Solve Begin transaction
        _nPURepository.Create(dao);
        _partRepository.CreateBatch(npu.Parts, dao.Id);
        //TODO: Solve Commit transaction
        return new NPUResponseDto(
            dao.Id,
            user.ConvertToUserResponseDto(),
            dao.Description,
            _fileStorage.GetUri(dao.ImageCode),
            dao.IsEnabled,
            dao.CreationDateTime,
            dao.UpdateDateTime,
            npu.Parts,
            averageCreativityRating: null,
            creativityRatings: 0,
            averageUniquenessRating: null,
            uniquenessRatings: 0);
      
    }

    public ErrorOr<NPUResponseDto> Get(Guid id, Guid userId)
    {
        var executingUser = _userRepository.Get(userId);

        if (executingUser == null)
        {
            return Errors.User.UserNotFound;
        }

        var dao = executingUser.IsAdmin ? _nPURepository.Get(id) : _nPURepository.GetEnabled(id);
        if (dao == null) { return Errors.NPU.NotFound; }

        var parts = _partRepository.GetForNPU(id);

        var user = _userRepository.Get(dao.UserId);
        if (user == null) { return Errors.NPU.CreatorNotFound; }

        var creativity = _creativityRatingRepository.GetRatingsForNpu(id);
        var uniquness = _uniquenessRatingRepository.GetRatingsForNpu(id);

        return new NPUResponseDto(
            dao.Id,
            user.ConvertToUserResponseDto(),
            dao.Description,
            _fileStorage.GetUri(dao.ImageCode),
            dao.IsEnabled,
            dao.CreationDateTime,
            dao.UpdateDateTime,
            parts,
            creativity.averageRating,
            creativity.ratingCount,
            uniquness.averageRating,
            uniquness.ratingCount);
    }

    public ErrorOr<RatingDto> CreateCreativityRating(Guid npuId, Guid userId, int rating)
    {
        var executingUser = _userRepository.Get(userId);

        if (executingUser == null)
        {
            return Errors.User.UserNotFound;
        }

        var dao = executingUser.IsAdmin ? _nPURepository.Get(npuId) : _nPURepository.GetEnabled(npuId);
        if (dao == null) { return Errors.NPU.NotFound; }

        _creativityRatingRepository.CreateRating(npuId, userId, rating);
        var creativity = _creativityRatingRepository.GetRatingsForNpu(npuId);
        return new RatingDto()
        {
            AverageRating = creativity.averageRating,
            Ratings = creativity.ratingCount,
        };
    }

    public ErrorOr<RatingDto> CreateUniquenessRating(Guid npuId, Guid userId, int rating)
    {
        var executingUser = _userRepository.Get(userId);

        if (executingUser == null)
        {
            return Errors.User.UserNotFound;
        }

        var dao = executingUser.IsAdmin ? _nPURepository.Get(npuId) : _nPURepository.GetEnabled(npuId);
        if (dao == null) { return Errors.NPU.NotFound; }

        _uniquenessRatingRepository.CreateRating(npuId, userId, rating);
        var uniquness = _uniquenessRatingRepository.GetRatingsForNpu(npuId);
        return new RatingDto()
        {
            AverageRating = uniquness.averageRating,
            Ratings = uniquness.ratingCount,
        };
    }

    public ErrorOr<NPUListResponseDto> List(NPUListRequestDto filters, Guid userId)
    {
        var executingUser = _userRepository.Get(userId);

        if (executingUser == null)
        {
            return Errors.User.UserNotFound;
        }

        //Only Admins can have IsEnabled filter
        if (!executingUser.IsAdmin)
        {
            filters.IsEnabled = true;
        }

        var (list, totalCount) = _nPURepository.GetList(filters.PageSize, filters.PageNumber, filters.SearchInDescription, filters.UserName, filters.AboveAverageCreativityRating, filters.BelowAverageCreativityRating, filters.AboveAverageUniqunessRating, filters.BelowAverageUniqunessRating, filters.IsEnabled, filters.Parts);
        var ret = new List<NPUResponseDto>();
        foreach (var item in list)
        {
            ret.Add(item.ConvertToNPUResponseDto(_fileStorage.GetUri(item.ImageUrl)));
        }

        return new NPUListResponseDto(ret, filters.PageSize, totalCount, filters.PageNumber);

    }

    private List<Error> ValidateNPUCreateRequestDto(NPUCreateRequestDto dto)
    {
        var errors = new List<Error>();
        if (dto.Description == null || dto.Description.Length < NPUCreateRequestDto.MinDesctiptionLength || dto.Description.Length > NPUCreateRequestDto.MaxDesctiptionLength)
        {
            errors.Add(Errors.NPU.InvalidDescription);
        }

        foreach (var part in dto.Parts)
        {
            if (part.Length < NPUCreateRequestDto.MinPartsLength || part.Length > NPUCreateRequestDto.MaxPartsLength)
            {
                errors.Add(Errors.NPU.InvalidPart);
            }
        }
        
        return errors;
    }
}
