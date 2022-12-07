# The Tester

*C# Console Application for testing input elements made using Selenium.*

## Remarks

### Files

- You only need to add the **tester.json** file in the *bin/Debug* folder
- **tester.json** file should be made according to the example given bellow:
```
{
	"TestingsObject": [
		{
			"Url": "https://someurl.com",
			"NumberOfActions": 30,
			"DelayBetweenActions": 1,
			"DelayToNextObject": 20,
			"ActionsObject": [
				{
					"ActionXPath": "XPath",
					"ActionValue": "Value",
					"ActionRandomLength": 0,
					"ActionAction": "input"
				},
		}
	]
}
```
- properties in the json file are:
  - *TestingsObject* - array of objects to test.
  - *Url* - url of the inputs to check.
  - *NumberOfActions* - number of times to repeat the given task.
  - *DelayBetweenActions* - delay between actions for system to wait.
  - *DelayToNextObject* - delay to going to the next item in the list.
  - *ActionsObject* - array of actions to test.

- properties in the object **ActionsObject** are:
  - *ActionXPath* - xpath of the element to test.
  - *ActionValue* - value to send to the object, for click can be empty, for select is value (possible values are randomstring, randomnumbers and randomletters)
  - *ActionRandomLength* - when value is random, use that length number of characters
  - *ActionAction* - type of html element (input, click, select)
