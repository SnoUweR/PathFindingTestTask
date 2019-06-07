### Описание
Написано на JetBrains Rider 2019.1.1.
Для компиляции использовался: .NET Framework 4.5.1 Developer Pack.

CLI использует библиотеку Newtonsoft JSON.
Tests использует NUnit.

Пример входного файла для CLI:
```json
{
	"TopLeftCorner":{"X":0,"Y":0},
	"BottomRightCorner":{"X":10,"Y":11},
	"Roads":[
		{
			"Item1":{"X":2,"Y":0},
			"Item2":{"X":5,"Y":11}
		},
		{
			"Item1":{"X":0,"Y":4},
			"Item2":{"X":5,"Y":0}
		},
		{
			"Item1":{"X":7,"Y":11},
			"Item2":{"X":8,"Y":0}
		},
		{
			"Item1":{"X":0,"Y":8},
			"Item2":{"X":10,"Y":10}
		},
	]
}
```