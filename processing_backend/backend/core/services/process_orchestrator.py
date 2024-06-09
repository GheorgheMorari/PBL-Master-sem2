import time

from tqdm import tqdm

from processing_backend.backend.core.adapters.adapter_registry import AdapterRegistry
from processing_backend.backend.core.adapters.api.captioning_adapter import CaptioningAdapter
from processing_backend.backend.core.adapters.api.compressor_adapter import CompressorAdapter
from processing_backend.backend.core.adapters.processing_orchestrator import ProcessingOrchestrator
from processing_backend.backend.core.adapters.repositories.content_repository import MinioContentRepository
from processing_backend.backend.core.adapters.repositories.metadata_repository import MongoDBMetadataRepository
from processing_backend.backend.core.services import default_content_repository_retriever, \
    default_metadata_repository_retriever, get_default_captioning_adapter_retriever, \
    get_default_compressor_adapter_retriever
from processing_backend.backend.core.services.generate_keywords import generate_keywords

PROCESSING_ORCHESTRATOR_SLEEP_TIME = 1


def process_orchestrator():
    metadata_repository: MongoDBMetadataRepository = AdapterRegistry.get_adapter(MongoDBMetadataRepository,
                                                                                 default_metadata_repository_retriever)

    content_repository: MinioContentRepository = AdapterRegistry.get_adapter(MinioContentRepository,
                                                                             default_content_repository_retriever)

    captioning_adapter: CaptioningAdapter = AdapterRegistry.get_adapter(CaptioningAdapter,
                                                                        get_default_captioning_adapter_retriever)
    compressor_adapter: CompressorAdapter = AdapterRegistry.get_adapter(CompressorAdapter,
                                                                        get_default_compressor_adapter_retriever)

    while not ProcessingOrchestrator.stop_flag:
        image_ids = metadata_repository.find_image_ids_that_are_not_processed()

        for image_id in tqdm(image_ids):
            image_metadata = metadata_repository.read(image_id)
            image_content = content_repository.read(image_id)

            # Captioning
            caption = captioning_adapter.predict_caption(image_content)
            keywords = generate_keywords(caption)
            image_metadata.keywords = keywords

            # Compressor
            compressed_image = compressor_adapter.compress_image(image_content)
            image_metadata.compressed = True

            image_metadata.processed = True

            metadata_repository.update(image_id, image_metadata)
            content_repository.update(image_id, compressed_image)

        time.sleep(PROCESSING_ORCHESTRATOR_SLEEP_TIME)
