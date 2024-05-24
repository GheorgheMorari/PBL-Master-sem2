from PIL import Image

from processing_backend.backend.image_captioning.adapters.caption_model import CaptioningModel


def image_captioning(image: Image) -> str:
    """
    Generate a caption from an image using a caption model.
    :param image: Image object.
    :return: str.
    """
    return CaptioningModel.generate_caption(image)
