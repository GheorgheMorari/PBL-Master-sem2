from io import BytesIO

from minio import Minio

from processing_backend.backend.core.domain.image_file import B64ImageFile


class MinioContentRepository:
    def __init__(self, minio_client: Minio, bucket_name: str):
        self.minio_client = minio_client
        self.bucket_name = bucket_name

    def create(self, image_id: str, image_file: B64ImageFile) -> None:
        byte_data = image_file.to_bytes()
        self.minio_client.put_object(self.bucket_name, image_id, BytesIO(byte_data), len(byte_data))

    def read(self, image_id: str) -> B64ImageFile:
        response = self.minio_client.get_object(self.bucket_name, image_id)
        byte_data = response.read()
        return B64ImageFile.from_bytes(byte_data)

    def delete(self, image_id: str) -> None:
        self.minio_client.remove_object(self.bucket_name, image_id)

    def update(self, image_id: str, image_file: B64ImageFile) -> None:
        self.create(image_id, image_file)
