# markdown-composer
Composes [Markdown](https://guides.github.com/features/mastering-markdown/) file trees into a singular Markdown file.

## Elevator Speech
Wanna write a larger project in Markdown?
Wanna have Git integration capabilities?
Wanna use a Scrivener-like project structure?

Markdown Composer is the thing for you! Just write in Markdown using your editor of choice, then run Markdown Composer on the project's root directory.
You'll have a single complete Markdown file which you can then transform however you like with a tool like [pandoc](https://pandoc.org/).

## Motivation
So I'm a software dev. I'm rather scatterbrained and need help keeping my s--t in order. I'm also a hobbyist sci-fi writer too.
- Scrivener is the bee's knees!
  - stellar for composing a complete manuscript
  - But it's a bit of a hassle to integrate with Git and I just don't enjoy writing in it.
- Visual Studio Code is awesome!
  - I love writing Markdown in that and there are extensions for natural language grammar checking.
  - But if I want to write with a Scrivener-like project structure then there's going to be some tediousness in putting together all those markdown files into one nice manuscript.

Enter Markdown Composer, a simple little console application that can assemble all your markdown files into one.

## Simple Example
Use a SUMMARY.md, GitBook style.
Example File Tree:
- root/
  - part1/
    - chapter1.md
    - chapter2.md
  - part2/
    - chapter1.md
    - chapter2.md
  - final_chapter.md
  - SUMMARY.md

Example Project Root SUMMARY.md
``` markdown
# Summary
* [Part 1](part1)
  * [Chapter 1](chapter1.md)
  * [Chapter 2](chapter2.md)
* [Part 2](part2)    
    * [Chapter 1](chapter1.md)    
    * [Chapter 2](chapter2.md)
* [Final Chapter](final_chapter.md)
```
Expected Output:
``` markdown
# Summary
## Part 1
### Chapter 1
- Chapter 1 content...
### Chapter 2
- Chapter 2 content...
## Part 2
### Chapter 1
- Chapter 1 content...
### Chapter 2
- Chapter 2 content...
## Final Chapter
- Final chapter content...
```
