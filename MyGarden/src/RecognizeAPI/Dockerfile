FROM python:3.12-slim

RUN apt-get update && apt-get -y install tesseract-ocr \
    && apt-get -y install tesseract-ocr-rus \
    && apt-get -y install curl \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*


WORKDIR /app
EXPOSE 80

WORKDIR /src
COPY ["src/RecognizeAPI/", "./"]

RUN pip install --no-cache-dir -r requirements.txt

CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "80"]
