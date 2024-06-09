from pydantic import BaseModel


class ImageMetadata(BaseModel):
    user_id: str
    keywords: list[str] = None
    compressed: bool = False
    processed: bool = False
