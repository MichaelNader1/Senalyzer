#!/usr/bin/env python
# coding: utf-8

import sys
from llamaapi import LlamaAPI
from openai import OpenAI

def analyze_sentiment(review):
    client = OpenAI(
        api_key="LL-1RC7LxETXTYzdwml76VfaP4ynaZunA7vsXXOTL9n0gMzuGB2VnJgb85xhAMKmlnv",
        base_url="https://api.llama-api.com"
    )

    response = client.chat.completions.create(
        model="llama-13b-chat",
        messages=[
            {"role": "system", "content": "translate to english then Mention positive as the first word if the next sentiment is positive otherwise write negative the first word if the sentiment is negative "},
            {"role": "user", "content": review}   
        ]
    )

    response_message = response.choices[0].message.content

    if "positive" in response_message.lower():
        return "positive review"
    else:
        return "negative review"

if __name__ == "__main__":
    if len(sys.argv) != 2:
        print("Usage: python script.py <review>")
        sys.exit(1)

    review = sys.argv[1]
    sentiment = analyze_sentiment(review)
    print(sentiment)
