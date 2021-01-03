Feature: Returning locations data based on country and zip code
	As a customer of the Zippopotam.us API
	I want to receive Location data matching the country code and zip code I supply
	So that I can use this data to auto-complete forms on my website
	
	Background: Specify country and zip code
		Given the country code is US and the zip code 90210
		When I request the locations corresponding to these codes

	Scenario: An existing country and zip code yields the correct place name
		Then the response contains the place name Beverly Hills

	Scenario: An existing country and zip code yields the right number of results
		Then the response contains exactly 1 location

	Scenario: An existing country and zip code yields the right HTTP status code
		Then the response has a status code 200