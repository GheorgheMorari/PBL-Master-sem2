import os

import dotenv
from pymongo import MongoClient

from processing_backend.backend.core.adapters.repositories.metadata_repository import MongoDBMetadataRepository

dotenv.load_dotenv()


def default_metadata_repository_retriever():
    mongo_client = MongoClient(os.getenv("METADATA_REPOSITORY_MONGO_DB_AUTH_URL"))
    database_name = os.getenv("METADATA_REPOSITORY_MONGO_DB_DATABASE_NAME")
    collection_name = os.getenv("METADATA_REPOSITORY_MONGO_DB_COLLECTION_NAME")
    return MongoDBMetadataRepository(mongo_client, database_name, collection_name)
