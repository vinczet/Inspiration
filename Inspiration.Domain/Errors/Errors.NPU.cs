using ErrorOr;
using Inspiration.Contract;

namespace Inspiration.Domain;

public static partial class Errors
{
    public static class NPU
    {
        public static Error NotFound => Error.NotFound(
            code: "NicePartUsage.NotFound",
            description: "NicePartUsage not found");
        public static Error CreatorNotFound => Error.NotFound(
            code: "NicePartUsage.CreatorNotFound",
            description: "Creator of NicePartUsage not found");
        public static Error InvalidDescription => Error.Validation(
            code: "NicePartUsage.InvalidDescription",
            description: $"Description should be between {NPUCreateRequestDto.MinDesctiptionLength} and {NPUCreateRequestDto.MaxDesctiptionLength} characters long.");
        public static Error InvalidPart => Error.Validation(
            code: "NicePartUsage.InvalidPart",
            description: $"Part should be between {NPUCreateRequestDto.MinPartsLength} and {NPUCreateRequestDto.MaxPartsLength} characters long.");
        public static Error AttachedFileNotImage => Error.Validation(
            code: "NicePartUsage.AttachedFileNotImage",
            description: "Attached file is not an image");

        public static Error NoFileAttached=> Error.Validation(
            code: "NicePartUsage.NoFileAttached",
            description: "No file attached");

        public static Error CouldNotSaveImage => Error.Validation(
            code: "NicePartUsage.CouldNotSaveImage",
            description: "Could not save image");

    }
}
