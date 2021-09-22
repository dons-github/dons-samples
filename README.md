# dons-samples

This solution is composed of three projects.

The SampleContact.Api project contains the entry points and only logic related to fulfilling those needs.

The SampleContact.Data project is the business tier and contains all the business logic, validation, and transforms.
Within the business tier, the Services control the flow of the requests as well as validation
Also in the business tier, the Repositories manage all interaction with external services, databases, etc

The SampleContact.Tests project contains the current test suite for the solution.