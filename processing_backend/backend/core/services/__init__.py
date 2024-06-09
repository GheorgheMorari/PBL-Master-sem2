import os

import dotenv
from minio import Minio
from pymongo import MongoClient

from processing_backend.backend.core.adapters.api.captioning_adapter import CaptioningAdapter
from processing_backend.backend.core.adapters.api.compressor_adapter import CompressorAdapter
from processing_backend.backend.core.adapters.repositories.content_repository import MinioContentRepository
from processing_backend.backend.core.adapters.repositories.metadata_repository import MongoDBMetadataRepository

dotenv.load_dotenv()


def default_metadata_repository_retriever():
    mongo_client = MongoClient(os.getenv("METADATA_REPOSITORY_MONGO_DB_AUTH_URL"))
    database_name = os.getenv("METADATA_REPOSITORY_MONGO_DB_DATABASE_NAME")
    collection_name = os.getenv("METADATA_REPOSITORY_MONGO_DB_COLLECTION_NAME")
    return MongoDBMetadataRepository(mongo_client, database_name, collection_name)


def default_content_repository_retriever():
    s3_endpoint = os.getenv("CONTENT_REPOSITORY_S3_ENDPOINT")
    s3_access_key = os.getenv("CONTENT_REPOSITORY_S3_ACCESS_KEY")
    s3_secret_key = os.getenv("CONTENT_REPOSITORY_S3_SECRET_KEY")

    minio_client = Minio(s3_endpoint, access_key=s3_access_key, secret_key=s3_secret_key, secure=False)

    bucket_name = os.getenv("CONTENT_REPOSITORY_S3_BUCKET_NAME")

    return MinioContentRepository(minio_client, bucket_name)


def get_default_captioning_adapter_retriever():
    captioning_service_api_url = os.getenv("CAPTIONING_API_URL")

    return CaptioningAdapter(captioning_service_api_url)


def get_default_compressor_adapter_retriever():
    compressor_service_api_url = os.getenv("COMPRESSOR_API_URL")

    return CompressorAdapter(compressor_service_api_url)
