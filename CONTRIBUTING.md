# How to contribute to the ayayaa project

## Reporting Bugs
All bugs are tracked as [GitHub Issues](https://guides.github.com/features/issues/)
**You found a bug? Great!**
* Check if the bug wasn't already reported by searching the [Issues](https://github.com/texel-sensei/ayayaa/issues).
* [Open a new Issue](https://github.com/texel-sensei/ayayaa/issues/new). Make sure you include a **title and clear description**
  on the bug, how to **reproduce it** if possible and your **expectations** and **what actually happens**.
  
## Contributing Code
**Please follow the coding guidelines**

### Workflow and Branches
This project uses the [Git-Flow](https://jeffkreeftmeijer.com/git-flow/) branching model. The
[master](https://github.com/texel-sensei/ayayaa/tree/master) branch contains only released versions. New features start their live on
the [develop](https://github.com/texel-sensei/ayayaa/tree/develop) branch and are worked on in their own feature brances.

When creating a feature branch, please **include the issue number in the branchname**.
Once a feature is finished, the feature branch is then merged into develop. When the develop branch is stable and ready for a new release
it is merged into master.

### Commit messages and SemVer
The ayayaa project uses [conventional commit messages](https://conventionalcommits.org/) and [Semantic Versioning](https://semver.org/).

**Anatomy of a commit:**
```
<type>[scope]: <Title>

<Body>

<Footer>
```
* Each commit message consists of a mandatory **header**, **body** and optional **footer**.
* Use the present tense in the commit message ("Add" not "Added")
* Use the imperative mood ("Send 404 response..." not "Send**s** 404 response...")
* Limit the header (first line) to 72 characters or less.
* Add `BREAKING CHANGE:` to the Footer if the commit adds changes that are not backwards compatible
  or need migration work by the user. Offer an in depth explanation on what has changed and *how to adapt*
  to new changes. **Avoid breaking changes** whenever possible!

|           Commit type  | Description                                                                         |
| ---------------------- |------------------------------------------------------------------------------------ |
| feat                   | Commit introduces new functionality                                                 |
| fix                    | Bugfixes                                                                            |
| docs                   | New/improved documentation                                                          |
| perf                   | Perfomance improvements                                                             |
| test                   | New/Updated tests                                                                   |
| refactor               | Changes in inner workings that do not change existing functionality                 |
| chore                  | Changes that are not directly code, but have to be done (e.g. CI, build system, etc)|
