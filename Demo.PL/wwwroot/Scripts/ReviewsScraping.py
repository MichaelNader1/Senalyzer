import os
import sys
import requests
import json
from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.by import By
from selenium.common.exceptions import NoSuchElementException
from time import sleep

def scrape_reviews(url, api_endpoint, chromedriver_path):
    sys.path.append(r'C:\Users\LapStore\Desktop\IronPython_Lib')

    if not os.path.isfile(chromedriver_path):
        raise FileNotFoundError(f"ChromeDriver not found at {chromedriver_path}")

    service = Service(chromedriver_path)
    driver = webdriver.Chrome(service=service)
    driver.get(url)
    driver.maximize_window()
    
    try:
        more_review_button = driver.find_element(By.XPATH, "(//a[@class='a-link-emphasis a-text-bold'])")
        more_review_button.click()
        reviews = []
        for i in range(100):
            review_elements = driver.find_elements(By.XPATH, "//span[@class='a-size-base review-text review-text-content']")
            for review in review_elements:
                reviews.append(review.text)
            try:
                next_button = driver.find_element(By.XPATH, "//li[@class='a-last']/a")
                next_button.click()
                sleep(10)
            except NoSuchElementException:
                break

        driver.quit()

        response = requests.post(api_endpoint, json={'reviews': reviews})
        
        if response.status_code == 200:
            result = response.json()
            return json.dumps({
                'positive_percentage': round(result['positive_percentage'], 2),
                'negative_percentage': round(result['negative_percentage'], 2)
            })
        else:
            return json.dumps({'error': 'Failed to send data. Status code: ' + str(response.status_code)})

    except NoSuchElementException:
        return json.dumps({'error': 'No reviews found'})

if __name__ == "__main__":
    url = sys.argv[1]
    api_endpoint = "https://scrapping-qc6w.onrender.com/submit-reviews"
    chromedriver_path = r'C:\Users\LapStore\Desktop\chromedriver-win64\chromedriver-win64\chromedriver.exe'
    print(scrape_reviews(url, api_endpoint, chromedriver_path))
