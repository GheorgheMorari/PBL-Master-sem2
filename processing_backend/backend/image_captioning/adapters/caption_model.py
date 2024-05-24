import os

import dotenv
import torch
from PIL import Image
from uform.torch_decoders import VLMProcessor, VLMForCausalLM

from processing_backend.backend.image_captioning.resources.models import CAPTIONING_MODELS_DIR_PATH

dotenv.load_dotenv()

CAPTIONING_MODEL_NAME = "unum-cloud/uform-gen"
CAPTIONING_MODEL_USE_CUDA = os.environ.get("CAPTIONING_MODEL_USE_CUDA", "false").lower() == 'true'

CAPTIONING_DEVICE = torch.device("cuda") if CAPTIONING_MODEL_USE_CUDA else torch.device("cpu")
CAPTIONING_DEFAULT_PROMPT_LIST = ["[cap] Summarize the visual content of the image."]


class CaptioningModel:
    model = None
    processor = None

    @classmethod
    def init_model(cls):
        if cls.model is None:
            cls.model = VLMForCausalLM.from_pretrained(CAPTIONING_MODEL_NAME, cache_dir=CAPTIONING_MODELS_DIR_PATH)
            cls.model.to(CAPTIONING_DEVICE)
            cls.processor = VLMProcessor.from_pretrained(CAPTIONING_MODEL_NAME, cache_dir=CAPTIONING_MODELS_DIR_PATH)

    @classmethod
    def generate_caption(cls, image: Image) -> str:
        cls.init_model()
        inputs = cls.processor(texts=CAPTIONING_DEFAULT_PROMPT_LIST, images=[image], return_tensors="pt")
        inputs.to(CAPTIONING_DEVICE)

        with torch.inference_mode():
            output = cls.model.generate(**inputs, do_sample=False, use_cache=True, max_new_tokens=128,
                                        eos_token_id=32001, pad_token_id=cls.processor.tokenizer.pad_token_id)

        prompt_len = inputs["input_ids"].shape[1]
        return cls.processor.batch_decode(output[:, prompt_len:], skip_special_tokens=True)[0]


CaptioningModel.init_model()
