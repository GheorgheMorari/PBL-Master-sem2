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

    def to_pil_image(self) -> Image.Image:
        return Image.open(io.BytesIO(base64.b64decode(self.image_file_data)))
