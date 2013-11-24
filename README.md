# TFS Lamp #

## Summary ##

If you're in the situation where you have to use TFS, then you may find TFS Lamp useful. It's a small utility that I bashed together to output (as html) the work items and changesets that are different between two branches or between two changeset numbers. It basically builds a tree of work items, their changesets and their parents that have been commited against either on the "from / source" branch or in the range of changesets.

TFS Lamp tries to "shed a bit of light" on things, because I found that I wanted an overall picture/snapshot instead of having to dig through loads of screens in visual studio. The output structures the work items in their hierarchy. It shows which changesets were linked to which work item, as well as the full list of changesets and their descriptions. The work items include their titles, type and id, and the changsets include their id, and description. Both work items and changesets link to the original in TFS so you can dig deeper.

If you find any bugs or have suggestions, add them as issues in github. Feel free to send pull requests etc. 

## General Usage Notes ##

Please note that the -mode option is always mandatory, and MUST be the first argument. This restriction will be rectified in the future.

## Merge Candidate Usage ##

| OPTION | TYPE | DESCRIPTION |
| ------ | ---- | ----------- |
| -mode(-m) | Mode | In this case specify "MergeCandidates" |
| -server(-s) | string | The TFS server to connect to. |
| -username(-u) | string | The username to use when connecting to the TFS server. |
| -password(-p) | string | The password to use when connecting to the TFS server. |
| -outputfile(-o) | string | The full path and file name of the output file to generate. If it exists it will be overwriten. |
| -frombranch(-fb) | string | The from or source branch. |
| -tobranch(-tb) | string | The to or target branch. |

## Changeset Range Usage ##

| OPTION | TYPE | DESCRIPTION |
| ------ | ---- | ----------- |
| -mode(-m) | Mode | In this case specify "ChangesetRange" |
| -server(-s) | string | The TFS server to connect to. |
| -username(-u) |string | The username to use when connecting to the TFS server. |
| -password(-p) |string | The password to use when connecting to the TFS server. |
| -outputfile(-o) |string | The full path and file name of the output file to generate. If it exists it will be overwriten. |
| -branch(-b) |string | The source branch. |
| -fromchangeset(-fc) | int | The from or earliest / lowest changeset. |
| -tochangeset(-tc) | int | The to or latest / highest changeset. |

## Merged Changeset Usage ##

| OPTION | TYPE | DESCRIPTION |
| ------ | ---- | ----------- |
| -mode(-m) | Mode | In this case specify "MergedChangeset" |
| -server(-s) | string | The TFS server to connect to. |
| -username(-u) |string | The username to use when connecting to the TFS server. |
| -password(-p) |string | The password to use when connecting to the TFS server. |
| -outputfile(-o) |string | The full path and file name of the output file to generate. If it exists it will be overwriten. |
| -frombranch(-fb) |string | The from or source branch. If using fromchangeset and tochangeset use this argument to specify the branch to use. |
| -tobranch(-tb) |string | The to or target branch. |
| -changeset(-c) | int | The merge changeset. |

## Example usage of Merge Candidates ##

```
TfsLamp -mode "MergeCandidates" -server http://myserver:8080/tfs/dev -username mydomain\user -password mypassword -fb "$/My Project Collection/branches/SourceBranch" -tb "$/My Project Collection/branches/SourceBranch" -outputfile C:\temp\output.html
```

## Example of changeset range ##

```
TfsLamp -m "ChangesetRange" -server http://myserver:8080/tfs/dev -username mydomain\user -password mypassword -b "$/My Project Collection/branches/SomeBranch" -fc 8963 -tc 9914 -o C:\temp\output.html
```

## Example of merged changeset ##

```
TfsLamp -m "MergedChangeset" -server http://myserver:8080/tfs/dev -username mydomain\user -password mypassword -fb "$/My Project Collection/branches/SourceBranch" -tb "$/My Project Collection/branches/SourceBranch" -c 9914 -o C:\temp\output.html
```

## Thanks ##

TFS Lamp uses some open source projects that include:

* Westwind.RazorHosting: https://github.com/RickStrahl/Westwind.RazorHosting
* Autofac: https://code.google.com/p/autofac/
* MSpec: https://github.com/machine/machine.specifications
* Power Args: https://github.com/adamabdelhamed/PowerArgs