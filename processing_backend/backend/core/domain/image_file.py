import base64
import io

from PIL import Image
from pydantic import BaseModel


class B64ImageFile(BaseModel):
    image_file_data: bytes
    width: int
    height: int

    @classmethod
    def from_pil_image(cls, image: Image.Image, quality=75) -> 'B64ImageFile':
        io_byte_array = io.BytesIO()
        image.save(io_byte_array, format="JPEG", quality=quality)
        return cls(image_file_data=base64.b64encode(io_byte_array.getvalue()), width=image.width, height=image.height)

    @classmethod
    def from_bytes(cls, image_bytes: bytes) -> 'B64ImageFile':
        image_file_data = base64.b64encode(image_bytes)
        pil_image = Image.open(io.BytesIO(image_bytes))
        return cls(image_file_data=image_file_data, width=pil_image.width, height=pil_image.height)

    def to_pil_image(self) -> Image.Image:
        return Image.open(io.BytesIO(base64.b64decode(self.image_file_data)))

    def to_bytes(self) -> bytes:
        return base64.b64decode(self.image_file_data)
