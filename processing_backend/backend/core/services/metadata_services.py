from processing_backend.backend.core.adapters.repositories.metadata_repository import MongoDBMetadataRepository
from processing_backend.backend.core.adapters.adapter_registry import AdapterRegistry
from processing_backend.backend.core.domain.backend_inputs import IngestInput, DeleteInput, QueryInput
from processing_backend.backend.core.domain.image_metadata import ImageMetadata
from processing_backend.backend.core.services import default_metadata_repository_retriever
from processing_backend.backend.core.services.generate_keywords import generate_keywords


def ingest_images_with_ingest_input(ingest_input: IngestInput):
    metadata_repository: MongoDBMetadataRepository = AdapterRegistry.get_adapter(MongoDBMetadataRepository,
                                                                                 default_metadata_repository_retriever)
    for image_id, user_id in zip(ingest_input.image_ids, ingest_input.user_ids):
        image_metadata = ImageMetadata(user_id=user_id)
        metadata_repository.create(image_id, image_metadata)


def delete_images_with_delete_input(delete_input: DeleteInput):
    metadata_repository: MongoDBMetadataRepository = AdapterRegistry.get_adapter(MongoDBMetadataRepository,
                                                                                 default_metadata_repository_retriever)
    for image_id in delete_input.image_ids:
        metadata_repository.delete(image_id)


def query_images_with_query_input(query_input: QueryInput):
    metadata_repository: MongoDBMetadataRepository = AdapterRegistry.get_adapter(MongoDBMetadataRepository,
                                                                                 default_metadata_repository_retriever)
    keywords = generate_keywords(query_input.query_string)
    return metadata_repository.find_image_ids_based_on_combined_location_keywords(query_input.user_id, keywords)
