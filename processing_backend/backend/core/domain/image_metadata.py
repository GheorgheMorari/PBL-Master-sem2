from typing import Optional

from pydantic import BaseModel


class ImageMetadata(BaseModel):
    user_id: str
    keywords: Optional[list[str]] = None
    compressed: bool = False
    processed: bool = False
