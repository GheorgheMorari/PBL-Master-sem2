from typing import List

from pymongo import MongoClient

from processing_backend.backend.core.domain.image_metadata import ImageMetadata


class MongoDBMetadataRepository:
    def __init__(self, mongo_client: MongoClient, database_name: str, collection_name: str):
        self.mongo_client = mongo_client
        self.database_name = database_name
        self.collection_name = collection_name
        self.collection = self.mongo_client[self.database_name][self.collection_name]

    def create(self, image_id: str, image_metadata: ImageMetadata) -> None:
        self.collection.insert_one({"_id": image_id, **image_metadata.model_dump()})

    def read(self, image_id: str) -> ImageMetadata:
        return ImageMetadata(**self.collection.find_one({"_id": image_id}))

    def delete(self, image_id: str) -> None:
        self.collection.delete_one({"_id": image_id})

    def update(self, image_id: str, image_metadata: ImageMetadata) -> None:
        self.collection.update_one({"_id": image_id}, {"$set": image_metadata.model_dump()})

    def find_image_ids_based_on_keywords(self, user_id: str, keywords: List[str]) -> List[str]:
        return self._find_image_ids_based_on_array_key(user_id, "keywords", keywords)

    def find_image_ids_based_on_location(self, user_id: str, location: List[str]) -> List[str]:
        return self._find_image_ids_based_on_array_key(user_id, "location", location)

    def find_image_ids_based_on_combined_location_keywords(self, user_id: str, combined_location_keywords: List[str]) -> \
            List[str]:
        return self._find_image_ids_based_on_array_key(user_id, "combined_location_keywords",
                                                       combined_location_keywords)

    def _find_image_ids_based_on_array_key(self, user_id: str, key: str, value: List[str]) -> List[str]:
        cursor = self.collection.find({"user_id": user_id, key: {"$all": value}}, {"_id": 1})
        return [doc["_id"] for doc in cursor]

    def find_image_ids_that_are_not_processed(self) -> List[str]:
        cursor = self.collection.find({"processed": False}, {"_id": 1})
        return [doc["_id"] for doc in cursor]

    def find_image_ids_that_should_get_keywords(self) -> List[str]:
        cursor = self.collection.find({"keywords": None}, {"_id": 1})
        return [doc["_id"] for doc in cursor]
