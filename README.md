# The Tester

*C# Console Application for testing input elements made using Selenium.*

## Remarks

### Files
- you need to create an json object named *tester.json* which is a **List<TestingObject>** with the properties bellow:
  - *Url* - url of the inputs to check.
  - *NumberOfActions* - number of times to repeat the given task.
  - *DelayBetweenActions* - delay between actions for system to wait.
  - *DelayToNextObject* - delay to going to the next item in the list.
  - *List<ActionsObject>* - array of **ActionsObject**.
  - *Url* - url to navigate to.
  - *Value* - value expected to get on the element.
  - *XPath* - FullXPath of the element to check.

- properties in the object **ActionsObject** are:
  - *ActionXPath* - xpath of the element to test.
  - *ActionValue* - value to send to the object, for click can be empty, for select is value (possible values are randomstring, randomnumbers and randomletters)
  - *ActionRandomLength* - when value is random, use that length number of characters
  - *ActionAction* - type of html element (input, click, select)
