import requests
import json
from bs4 import BeautifulSoup

# access html by Requests
url = 'http://163.19.32.13/cgi-bin/sudoku/make_sudo/make_sudo81.cgi?sudo+20+3+100+e' # Google serach URL
my_headers = {'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36'}
numberOfDocScraped= 10000

f = open("easy.txt", "a+")

r = requests.get(url, headers = my_headers)
if r.status_code == requests.codes.ok:
    html = r.text
    html_soup = BeautifulSoup(html, 'html.parser')
    for item in html_soup.find_all('table', {'border':'0'}):
        for item_typeItem in item.find_all('table', {'style':'border-collapse:collapse;border:solid black 2pt;'}):
            for item_contentItem in item_typeItem.find_all('tr', {'style':'height:22pt'}):
                if(item_contentItem):
                    item_content = item_contentItem.get_text(strip=True)
                    f.write(item_content + "\n")

f.close()

f = open("normal.txt", "a+")
url = 'http://163.19.32.13/cgi-bin/sudoku/make_sudo/make_sudo81.cgi?sudo+20+3+100+m'
r = requests.get(url, headers = my_headers)
if r.status_code == requests.codes.ok:
    html = r.text
    html_soup = BeautifulSoup(html, 'html.parser')
    for item in html_soup.find_all('table', {'border':'0'}):
        for item_typeItem in item.find_all('table', {'style':'border-collapse:collapse;border:solid black 2pt;'}):
            for item_contentItem in item_typeItem.find_all('tr', {'style':'height:22pt'}):
                if(item_contentItem):
                    item_content = item_contentItem.get_text(strip=True)
                    f.write(item_content + "\n")

f.close()