# TfL Journey Planner Test Automation

This repository contains a test automation framework built with C#, SpecFlow, and Playwright for testing the TfL Journey Planner widget.

## Decisions
- **PlayWrite**: Chosen for its versatility and integration with SpecFlow.
- **SpecFlow**: Used for creating readable and maintainable test cases in Gherkin syntax.

## Setup Instructions

1. Clone this repository.
2. Open the solution in Visual Studio.
3. Restore NuGet packages.
4. Run the tests using the Test Explorer.

## Development Decisions

- Used Playwright for its robustness in handling asynchronous page events and UI interactions.
- SpecFlow and Gherkin syntax were used to create clear and maintainable test cases.
- The tests focus on core functionality, including error handling and accessibility information.
# TfL Journey Planner Automation

## Overview
This automation framework tests the Journey Planner widget on the TfL website using Selenium WebDriver, SpecFlow, and C#. It covers various functional and non-functional scenarios to ensure the widget operates correctly and efficiently.

## Non-Functional Test Scenarios

### Load Testing
- Simulate a high number of users accessing the Journey Planner simultaneously and observe how the system performs under load to ensure it can handle peak traffic.

### Usability Testing
- Evaluate the user interface for ease of navigation and accessibility. This can include checking font sizes, contrast ratios, and the overall flow of planning a journey.

### Accessibility Testing
- Test the Journey Planner with screen readers and keyboard navigation to ensure that it is accessible to users with disabilities, complying with WCAG (Web Content Accessibility Guidelines) standards.

## Functional Scenarios

### Check Journey Planner Auto-Suggest Feature
- Enter partial station names and verify that the auto-suggest feature provides relevant suggestions,
including validating the selection of a suggestion.

### Verify Journey Planner for Return Journeys
- Plan a journey from "Covent Garden Underground Station" back to 
"Leicester Square Underground Station" and validate the travel times and details.

### Validate Error Messages for Invalid Input
- Input invalid characters or nonsensical locations (e.g., "12345", "special characters") and verify that appropriate error messages are displayed.


## Author
Adetokumbo Ajayi
