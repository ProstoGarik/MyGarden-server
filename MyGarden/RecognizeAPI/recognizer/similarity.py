from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity
import Levenshtein
from concurrent.futures import ThreadPoolExecutor

def calculate_tfidf_similarity(input_text, json_texts):
    texts = [input_text] + json_texts
    vectorizer = TfidfVectorizer()
    tfidf_matrix = vectorizer.fit_transform(texts)
    cosine_sim = cosine_similarity(tfidf_matrix[0:1], tfidf_matrix[1:]).flatten()
    return cosine_sim

def levenshtein_similarity_worker(input_text, text):
    distance = Levenshtein.distance(input_text, text)
    max_len = max(len(input_text), len(text))
    return 1 - distance / max_len if max_len > 0 else 1

def calculate_levenshtein_similarity(input_text, json_texts):
    with ThreadPoolExecutor() as executor:
        results = list(executor.map(lambda text: levenshtein_similarity_worker(input_text, text), json_texts))
    return results


def extract_text(data):
    result = []
    if isinstance(data, dict):
        for value in data.values():
            result.extend(extract_text(value))
    elif isinstance(data, list):
        for item in data:
            result.extend(extract_text(item))
    elif isinstance(data, str):
        result.append(data)
    return ''.join(result)
