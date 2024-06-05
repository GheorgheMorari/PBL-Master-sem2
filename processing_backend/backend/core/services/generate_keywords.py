import nltk

nltk.download("punkt")
nltk.download("stopwords")

from nltk.corpus import stopwords
from nltk.tokenize import word_tokenize

stop_words = set(stopwords.words("english"))


def generate_keywords(text: str) -> list[str]:
    word_tokens = word_tokenize(text.lower())

    return [word for word in word_tokens if word not in stop_words]
