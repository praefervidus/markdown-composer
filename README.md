# markdown-composer
Enter Markdown Composer, a simple little console application that can assemble all your markdown files into one.
Composes [Markdown](https://guides.github.com/features/mastering-markdown/) file trees into a singular Markdown file.
Just write in Markdown using your editor of choice, then run Markdown Composer on the project's root directory.
You'll have a single complete Markdown file which you can then transform however you like with a tool like [pandoc](https://pandoc.org/).

## Basic Idea through a Simple Example
Use a SUMMARY.md in the root folder of your project, sort of like GitBook.
Example File Tree:
``` markdown
- root/
  - front_matter.md
  - part1/
    - chapter1.md
    - chapter2.md
  - part2/
    - intro.md
    - chapter1.md
    - chapter2.md
  - epilogue.md
  - glossary.md
  - end_matter.md
  - SUMMARY.md
```
### Example Project Root SUMMARY.md:
An asterisk denotes anything that should be used in the Table of Contents, and the number of asterisks denote it's indentation level.
Sections with higher indentation levels will have headings with more '#'s.
``` markdown
[Title](front_matter.md)
* [Part 1]()
* * [Chapter 1](part1/chapter1.md)
* * [Chapter 2](part1/chapter2.md)
* [Part 2](part2/intro.md)    
* * [Chapter 1](part2/chapter1.md)    
* * [Chapter 2](part2/chapter2.md)
* [Epilogue](epilogue.md)
* [Glossary](glossary.md)
[OptionalEndingText](end_matter.md)
```
Any link not given an asterisk is given a level one heading automatically (heading with one '#'). These aren't included in the Table of Contents.
When run with the *--toc* option, Markdown Composer will generate a Table of Contents from the list area.
Expected Output:
``` markdown
# Title
front matter...
## Table of Contents
* Part 1
  * Chapter 1
  * Chapter 2
* Part 2
  * Chapter 1    
  * Chapter 2
* Epilogue
* Glossary

## Part 1

### Chapter 1
Chapter 1 content...
### Chapter 2
Chapter 2 content...
## Part 2
part 2 intro text...
### Chapter 1
Chapter 1 content...
### Chapter 2
Chapter 2 content...
## Epilogue
Epilogue content...
## Glossary
glossary content...
# OptionalEndingText
end matter...
```
## Important Note on Links:
If you have a link in your text that is not your SUMMARY.md file, (usually in my case it's to a photo or something) like so:
``` markdown
// root/part1/chapter3/scene3-grandpa-buys-me-lollipop.md

random text...
  [ScaryPhoto](scaryphoto.jpg)
... lorem ipsum
```
When you run markdown-composer, that link is ***preserved as is***. So this means that your new composed file now has mismatched links because your scaryphoto.jpg was in your chapter folder, but your composed file is in your root folder .

### The Workaround
To fix this, I recommend a root-level resources folder you keep as you're writing.
``` markdown
- composed-output-v1.md
- composed-output-v2.md
- markdown-composer.exe
- root
  - resources
    - chp3scn3-scaryphoto.jpg
  - chapter 1
  - chapter 2
  - chapter 3
    - scene3-grandpa-buys-me-lollipop.md
  - SUMMARY.md
```
Ideally you should write your links as **absolute paths** to your images, but you can also 
write your links like below so long as you adhere to a similar file structure as above.
``` markdown
lorem ipsum ...

  + [scary-photo](root/resources/chp3scn3-scaryphoto.md)

... lorem ipsum
```
This way, you can run a tool like *pandoc* on that composed file and it will still be able to parse your links.
