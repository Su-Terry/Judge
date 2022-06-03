Judge
===

How to Download?
---
1. Click Releases on the right side and choose win10-x64.zip to download
2. unzip and you will get Judge.exe, Judge.pdb, and two other folders such as "io/", "proc/" (it's quite safe because it is made by me)

How to Use?
---
1. open "proc/" folder
2. put your PartI.h, PartI.cpp, PartII.h, PartII.cpp in the folder.
3. re-compile main.cpp \
    ```g++ -o main.exe main.cpp PartI.cpp PartII.cpp``` \
    (open cmd and paste)
4. Run Judge.exe.
**It doesn't matter if you use the cout object. (It will not cause WA)**
But BE CARE FOR something like **system** or **assert** that consumes lots of time.

What if the Judge told me something like "there is a latest version"
---
Just re-download it, everything will be fine.

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
