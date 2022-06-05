Judge
===

How to Download?
---
1. Click Releases on the right side and choose win10-x64-v2.0.0-beta.zip or Judge-Light.zip to download \
    (Noted that the Former has no more than 1MB and has another readme.txt in it.) \
    (Noted that pre-release version may have bug).
2. Unzip and you will get Judge.exe, Judge.pdb, and two other folders such as "io/", "proc/" (it's quite safe because it is made by me)

How to Use?
---
1. open "proc/" folder
2. put your Part1.h, Part1.cpp, Part2.h, Part2.cpp in the folder.
3. re-compile main.cpp \
    ```g++ -o main.exe main.cpp Part1.cpp Part2.cpp``` \
    (open cmd and paste)
4. Run Judge.exe. (Be sure that you are in the same directory and your directory path has no space in it.)
**It doesn't matter if you use the cout object. (It will not cause WA)** \
But *BE CARE* for something like **system** or **assert** that consumes lots of time.

What if I got WA and I want to look up the test data
---
The test datas are in the "io/" folder
in1.txt, in2.txt are inputs \
out1.txt, out2.txt are outputs by your program \
ans1.txt, ans2.txt are outputs from the judge

What if The Judge shows Judge_error
---
這代表作者自己把code寫爛了，請通知作者

Result
---
![image](https://user-images.githubusercontent.com/51773435/171774210-65e68feb-1a4a-4e10-b4e2-b657e319d962.png) \
If vertict not in { PENDING, AC }:
    you will get error_msg and test_id indicate on what test_id what type of error just occurred.
