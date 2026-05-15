# Git basics

Git tracks changes to your files over time. It lets you save snapshots of your work (called "commits"), undo mistakes, and collaborate with others.

## First-time setup

After installing Git, tell it your name and email (this labels your commits):

```bash
git config --global user.name "Your Name"
git config --global user.email "your@email.com"
```

## Key concepts

- **Repository (repo):** your project folder, tracked by Git
- **Commit:** a saved snapshot of your changes, with a message describing what changed
- **Branch:** a parallel line of development (you start on `main`)
- **Staging:** selecting which changes to include in the next commit

## Daily workflow

### 1. Check what changed

```bash
git status
```

This shows which files you modified, added, or deleted.

### 2. Stage your changes

```bash
git add src/Components/GH_MyComponent.cs    # stage one file
git add .                                     # stage everything
```

Staging means "mark these changes to be included in the next commit."

### 3. Commit

```bash
git commit -m "Add GH_MyComponent with basic input/output"
```

The message after `-m` should briefly describe what you changed and why.

### 4. Push to remote (if using GitHub/GitLab)

```bash
git push
```

This uploads your commits to the remote server so others can see them.

## Undoing things

```bash
git checkout -- src/MyFile.cs    # discard changes to one file (back to last commit)
git stash                        # temporarily hide all changes
git stash pop                    # bring them back
```

## Viewing history

```bash
git log --oneline               # compact list of commits
git diff                        # see what changed since last commit
```

## .gitignore

The `.gitignore` file tells Git which files to ignore. Build output (`build/`, `obj/`) is already ignored in this project. Never commit build artifacts.

## Pre-commit hook (auto-formatting)

This project has a pre-commit hook that automatically formats your C# code with CSharpier before each commit. You do not need to do anything - it happens automatically. If you see "Formatting staged C# files..." when committing, that is normal.

## Git vs. GitHub

Git and GitHub are not the same thing.

**Git** is the tool that runs on your computer. It tracks changes, creates commits, and manages branches. Git works entirely offline. You do not need an internet connection to commit, view history, or switch branches.

**GitHub** is a website (github.com) that hosts Git repositories online. It adds features on top of Git: a web interface to browse code, pull requests for code review, issues for bug tracking, and access control for teams. When you run `git push`, you are uploading your local commits to GitHub (or whichever hosting service you use).

You can use Git without GitHub. You can also use GitHub with a different Git client. They are independent.

### Setting up GitHub

1. Create an account at https://github.com
2. Create a new repository (click "+" in the top right corner)
3. Follow the instructions on the new repo page to connect your local project:

```bash
git remote add origin https://github.com/YourName/your-repo.git
git push -u origin main
```

After this, `git push` uploads your commits and `git pull` downloads commits others have pushed.

### Alternatives to GitHub

GitHub is the most popular option, but there are others:

| Service | Link | Notes |
|---------|------|-------|
| GitHub | https://github.com | Most widely used, free for public and private repos |
| GitLab | https://gitlab.com | Similar features, also offers self-hosting |
| Bitbucket | https://bitbucket.org | Integrates with Atlassian tools (Jira, Confluence) |

All three work the same way from Git's perspective. The `git push`, `git pull`, and `git clone` commands are identical regardless of which service hosts your repository. If you switch services later, you only need to update the remote URL.

Read more at https://git-scm.com/book/en/v2/Getting-Started-About-Version-Control
