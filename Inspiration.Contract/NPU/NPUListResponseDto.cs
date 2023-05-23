namespace Inspiration.Contract;

public record NPUListResponseDto(List<NPUResponseDto> NPUs, int? ItemsPerPage, int TotalCount, int? ActivePageNumber);
