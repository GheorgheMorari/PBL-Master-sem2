from pydantic import BaseModel


class ImageMetadata(BaseModel):
    keywords: list[str] = None
    location: list[str] = None
    combined_location_keywords: list[str] = None
    compressed: bool = False
    should_be_compresed: bool = True
