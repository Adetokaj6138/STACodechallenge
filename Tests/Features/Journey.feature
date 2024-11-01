Feature: TfL Journey Planner Widget

  As a user of the TfL website
  I want to be able to plan journeys using the "Plan a journey" widget
  So that I can view travel times and route information

  Background:
    Given I navigate to the TfL homepage
    And I accept cookies
    And I open the "Plan a journey" widget

  @ValidJourney
  Scenario: Plan a valid journey from "Leicester Square Underground Station" to "Covent Garden Underground Station"
    When I enter "Leicester Square" as the starting point
    And I enter "Covent Garden" as the destination
    And I click on plan a journey button
    Then I should see journey options for walking and cycling
    When I select a travel mode of "walking"
    Then I should see journey options with a valid journey time for both walking and cycling

  @EditPreferences
  Scenario: Edit journey preferences to choose routes with least walking
    When I enter "Leicester Square" as the starting point
    And I enter "Covent Garden" as the destination
    And I click on plan a journey button
    When I click on edit preferences
    When I edit journey preferences to select routes with "least walking"
    And I update the journey
    Then I should see the journey time updated according to the preference

  @ViewDetails
  Scenario: View complete access information for destination station
    When I enter "Leicester Square" as the starting point
    And I enter "Covent Garden" as the destination
    And I click on plan a journey button
    When I click on edit preferences
    When I edit journey preferences to select routes with "least walking"
    And I update the journey
    Then I should see complete access information at "Covent Garden Underground Station"

  @InvalidJourney
  Scenario: Attempt to plan a journey with invalid location(s)
    When I enter invalid value "ZASQZ AZWSX" as the starting point
    And I enter invalid value "~QZTQ BQZXA" as the destination
    Then I should see an error message indicating that no results were found

  @EmptyFields
  Scenario: Attempt to plan a journey without entering any locations
    When I click on plan a journey button
    Then I should see an error message indicating that locations are required
