using FluentAssertions;
using NUnit.Framework;
using RestSharp.DataEntries;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RestSharp.Steps
{
    [Binding]
    public class ReturningLocationsDataBasedOnCountryAndZipCodeSteps
    {
        RestClient client;
        RestRequest request;
        IRestResponse response;
        IEnumerable<Place> places;

        [Given(@"the country code is (.+) and the zip code (.*)")]
        public void GivenTheCountryCodeIsUSAndTheZipCode(string countryCode, string zipCode)
        {
            client = new RestClient("http://api.zippopotam.us");
            request = new RestRequest($"{countryCode}/{zipCode}", Method.GET);
        }

        [Given(@"the following places")]
        public void GivenTheFollowingPlaces(Table table)
        {
            places = table.CreateSet<Place>();
        }

        [When(@"I request the locations corresponding to these codes")]
        public void WhenIRequestTheLocationsCorrespondingToTheseCodes()
        {
            response = client.Execute(request);
        }
        
        [Then(@"the response contains the place name (.*)")]
        public void ThenTheResponseContainsThePlaceNameBeverlyHills(string expectedPlaceName)
        {
            LocationResponse locationResponse = new JsonDeserializer().Deserialize<LocationResponse>(response);
            //Assert.That(locationResponse.Places[0].PlaceName, Is.EqualTo(expectedPlaceName));
            locationResponse.Should().NotBeNull();
            locationResponse.Places[0].PlaceName.Should().Equals(expectedPlaceName);
        }
        
        [Then(@"the response contains exactly (\d+) location")]
        public void ThenTheResponseContainsExactlyLocation(int expectedNumberOfPlacesReturned)
        {
            LocationResponse locationResponse = new JsonDeserializer().Deserialize<LocationResponse>(response);
            //Assert.That(locationResponse.Places.Count, Is.EqualTo(expectedNumberOfPlacesReturned));
            locationResponse.Places.Count.Should().Equals(expectedNumberOfPlacesReturned);
        }
        
        [Then(@"the response has a status code (200|404|500)")]
        public void ThenTheResponseHasAStatusCode(int expectedStatusCode)
        {
            //Assert.That((int)response.StatusCode, Is.EqualTo(expectedStatusCode));
            response.StatusCode.Should().Equals(expectedStatusCode);
        }

        [Then(@"the response contains the following place")]
        public void ThenTheResponseContainsTheFollowingPlace(Table table)
        {
            LocationResponse locationResponse = new JsonDeserializer().Deserialize<LocationResponse>(response);
            table.CompareToInstance<Place>(locationResponse.Places[0]);
        }

        [Then(@"the response contains the following places")]
        public void ThenTheResponseContainsTheFollowingPlaces(Table table)
        {
            LocationResponse locationResponse = new JsonDeserializer().Deserialize<LocationResponse>(response);
            table.CompareToSet<Place>(locationResponse.Places);
            //table.CompareToSet(locationResponse.Places);
        }


    }
}
