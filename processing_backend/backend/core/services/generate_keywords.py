import nltk

nltk.download("punkt")
nltk.download("stopwords")
nltk.download("wordnet")

from nltk.corpus import stopwords
from nltk.tokenize import word_tokenize
from nltk.stem import WordNetLemmatizer

lemmatizer = WordNetLemmatizer()
stop_words = set(stopwords.words("english"))


def generate_keywords(text: str) -> list[str]:
    word_tokens = word_tokenize(text.lower())
    filtered_tokens = [word for word in word_tokens if word not in stop_words]
    return [lemmatizer.lemmatize(word) for word in filtered_tokens]
