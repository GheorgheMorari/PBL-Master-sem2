from typing import List

from pydantic import BaseModel


class IngestInput(BaseModel):
    image_ids: List[str]
    user_ids: List[str]


class DeleteInput(BaseModel):
    image_ids: List[str]


class QueryInput(BaseModel):
    user_id: str
    query_string: str
