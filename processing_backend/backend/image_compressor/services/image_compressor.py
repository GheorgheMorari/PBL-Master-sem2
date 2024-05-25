from processing_backend.backend.core.domain.image_file import B64ImageFile

IMAGE_COMPRESSION_QUALITY = 70


def compress_image(b64_image_file: B64ImageFile) -> B64ImageFile:
    original_image = b64_image_file.to_pil_image()
    return B64ImageFile.from_pil_image(original_image, quality=IMAGE_COMPRESSION_QUALITY)
