from typing import Optional
from urllib.parse import urljoin

import requests

from processing_backend.backend.core.domain.image_file import B64ImageFile
from processing_backend.backend.image_captioning.services import CAPTIONING_API_ENTRY_POINT_PREDICT_CAPTION_PATH


class CaptioningAdapter:

    def __init__(self, service_api_url: str, connection_timeout: Optional[float] = 5.0,
                 read_timeout: Optional[float] = 60.0):
        self.captioning_service_api_url = urljoin(service_api_url, CAPTIONING_API_ENTRY_POINT_PREDICT_CAPTION_PATH)
        self.timeout = (connection_timeout, read_timeout)

    def predict_caption(self, image_file: B64ImageFile) -> str:
        response = requests.post(self.captioning_service_api_url, data=image_file.model_dump_json(),
                                 timeout=self.timeout)

        if response.status_code != 200:
            raise Exception(response.text)

        return response.text
