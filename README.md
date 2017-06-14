# WebCrawler
In order to run the application you need to compile the source code.
Then you can run WebCrawler.exe from release folder.

This is a simple single thread data extractor.
The current implementation has only one parser to parse the results from https://finance.yahoo.com/world-indices
We can easily extend our application by implementing more Parsers.

The application will ask you to choose one of the following output formats, option 1 is for the CSV format and option 2 is for the 
Json format.

*****************
1. CSV Format
2. Json Format

Please select one of the above output formats (1 or 2).
*****************

After choosing the format, the application will send a GET request to this url: https://finance.yahoo.com/world-indices
then the Parse will parse the response and keep it as a list of dictionary.

Next, The application will convert the list of dictionary based on the selected output format(CSV or JSON). And finally, it will be saved as a file.
The console will show you where the file is located.

The default path for saved files is Outputs in application folder. ..\WebCrawler\Outputs

Output Fileds:
- Id
- Symbol
- Exchange
- Exchange Full Name
- Exchange Time Zone
- Regular Market Price
- Regular Market Volume
- Regular Market Change %
- Regular Market Time
- Market State


**********
The application consists of several unit tests and an integration test.

Most of the tests have been written for only positive cases.





