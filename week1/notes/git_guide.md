# Git: Zero to Hero

A complete, practical guide. Read it in order the first time, then use it as a reference.

**Levels**
- Part 1-3: Beginner (concepts, everyday commands, undoing)
- Part 4-6: Intermediate (branches, merging, remotes, pull requests)
- Part 7-10: Advanced (rebase, history surgery, recovery, debugging)
- Part 11-14: Hero (internals, workflows, hooks, CI, team practice)
- Part 15+: Cheat sheets, exercises, troubleshooting

---

# Part 1: Foundations

## 1.1 What is Git?

Git is a **distributed version control system**. It records snapshots of your project over time so you can:

- go back to any earlier state
- see who changed what and why
- work on many features in parallel without stepping on each other
- collaborate with hundreds of people on the same codebase

**Distributed** means every developer has a full copy of the entire history. You can commit, branch, and browse history offline. A server like GitHub is just one more copy that everyone agrees to treat as the shared one.

**Git vs GitHub:** Git is the tool. GitHub, GitLab, and Bitbucket are websites that host Git repositories and add features such as pull requests, issues, and CI.

## 1.2 The mental model (most important section)

1. **A commit is a snapshot** of your entire project at one moment, plus metadata: author, date, message, and a pointer to its parent commit(s).
2. **Commits form a graph.** Each commit points to its parent:

```
A <- B <- C <- D
```

3. **Every commit has an ID**: a SHA-1 hash (e.g. `a3f9c2e81b...`) computed from its content, metadata, and parent. Change anything and the hash changes, so history is tamper-evident.
4. **A branch is a movable pointer to a commit.** Nothing more. `main` is a label stuck to commit D. Make a new commit and the label moves forward.
5. **HEAD is a pointer to "where you are now."** Usually it points to a branch, which points to a commit.
6. **Three areas** every change passes through:

```
Working directory  --git add-->  Staging area (index)  --git commit-->  Repository (.git)
 (files you edit)                (what the next commit                  (permanent history)
                                  will contain)
```

The staging area lets you build a commit deliberately: edit 6 files, commit 2 now and 4 later.

## 1.3 Installing and configuring

Install: https://git-scm.com (Windows), `brew install git` (macOS), `sudo apt install git` (Debian/Ubuntu).

```bash
git --version

git config --global user.name  "Your Name"
git config --global user.email "you@example.com"
git config --global init.defaultBranch main
git config --global core.editor "code --wait"     # VS Code
git config --global pull.rebase false             # or true, see Part 6
git config --global core.autocrlf input           # macOS/Linux; use "true" on Windows
git config --global color.ui auto

git config --list --show-origin                   # see all settings and where they come from
```

Config levels: `--system` (whole machine), `--global` (your user), `--local` (this repo only, the default inside a repo). Local overrides global.

Use your GitHub-verified email, or GitHub's `noreply` address, so commits are attributed to you.

---

# Part 2: Everyday Commands

## 2.1 Starting a repository

```bash
git init                 # turn the current folder into a repo (creates .git/)
git init my-project      # create folder + repo
git clone <url>          # copy a remote repo (full history) and set up origin
git clone <url> folder   # clone into a specific folder
git clone --depth 1 <url>  # shallow clone: latest snapshot only (fast for huge repos)
```

The `.git/` directory **is** the repository. Delete it and the folder is just files again.

## 2.2 `git status`: your compass

```bash
git status
git status -s            # short format
```

Shows the current branch, staged changes, unstaged changes, and untracked files. Run it before and after almost every command until it's a reflex.

Short format legend: `??` untracked, `A` added, `M` modified, `D` deleted. Left column = staged, right column = unstaged.

## 2.3 `git add`: staging

```bash
git add file.js          # one file
git add src/ docs/       # folders
git add .                # everything under current directory
git add -A               # everything in the whole repo (new, modified, deleted)
git add -u               # only already-tracked files (modified/deleted)
git add -p               # interactive: pick individual hunks
```

**`git add -p` is a superpower.** If one file contains a bug fix and an unrelated refactor, stage only the fix. Choices at the prompt: `y` stage, `n` skip, `s` split into smaller hunks, `e` edit manually, `q` quit.

## 2.4 `git commit`

```bash
git commit -m "Add login endpoint"
git commit                    # opens editor for a longer message
git commit -am "Fix typo"     # stage all TRACKED changes + commit (ignores new files)
git commit --amend            # replace the last commit (new message and/or newly staged changes)
git commit --amend --no-edit  # add staged changes to last commit, keep message
git commit --allow-empty -m "Trigger CI"
```

Only amend commits you have **not** pushed (or only pushed to a branch only you use).

## 2.5 `git log`: reading history

```bash
git log
git log --oneline
git log --oneline --graph --all --decorate     # visual branch graph
git log -5                                     # last 5 commits
git log -p                                     # with diffs
git log --stat                                 # with files changed summary
git log --author="Kishan"
git log --since="2 weeks ago" --until="yesterday"
git log --grep="login"                         # search commit messages
git log -S"validateUser"                       # "pickaxe": commits that added/removed this string
git log -G"regex"                              # commits whose diff matches regex
git log -- path/to/file                        # history of one file
git log --follow -- path/to/file               # follow renames
git log main..feature                          # commits in feature but not in main
git log --format="%h %an %ar %s"               # custom format
```

Handy alias:

```bash
git config --global alias.lg "log --oneline --graph --all --decorate"
git lg
```

## 2.5b `git show`, `git blame`

```bash
git show                 # last commit with diff
git show a3f9c2e         # a specific commit
git show HEAD~2          # two commits before HEAD
git show main:src/app.js # a file as it was on main

git blame file.js        # who last changed each line, and in which commit
git blame -L 10,30 file.js
```

`blame` isn't about blaming people. It's about finding the commit message that explains *why* a line exists.

## 2.6 `git diff`

```bash
git diff                     # unstaged changes (working dir vs index)
git diff --staged            # staged changes (index vs last commit)  (= --cached)
git diff HEAD                # all uncommitted changes vs last commit
git diff main..feature       # tip of main vs tip of feature
git diff main...feature      # changes on feature since it diverged from main (what a PR shows)
git diff a3f9 b81d           # between two commits
git diff --stat              # summary
git diff --name-only         # just filenames
git diff -w                  # ignore whitespace
```

**Habit:** run `git diff --staged` before every commit to catch stray `console.log`, secrets, and accidental files.

## 2.7 `.gitignore`

A `.gitignore` file in the repo root lists files Git should never track:

```gitignore
# dependencies
node_modules/

# environment & secrets
.env
.env.*
!.env.example

# build output
dist/
build/
*.log

# OS / editor junk
.DS_Store
Thumbs.db
.vscode/
.idea/

# coverage
coverage/
```

Pattern rules:
- `*.log` any file ending in .log
- `build/` a directory
- `/todo.txt` only in the repo root
- `docs/**/*.pdf` PDFs anywhere under docs
- `!keep.log` negation (re-include)
- Lines starting with `#` are comments

**Critical gotcha:** `.gitignore` only affects **untracked** files. If you already committed something:

```bash
git rm --cached .env             # stop tracking, keep the file on disk
git rm -r --cached node_modules  # for folders
git commit -m "Stop tracking .env"
```

If a **secret** was ever committed, assume it's compromised. **Rotate it** (new API key/password). Removing it from history is secondary (see Part 9).

Other tools:
```bash
git check-ignore -v file.txt      # why is this file ignored?
git ls-files                      # list tracked files
git clean -n                      # DRY RUN: show untracked files that would be deleted
git clean -fd                     # actually delete untracked files and folders (dangerous)
```

Global ignore for your own junk (all repos): `git config --global core.excludesfile ~/.gitignore_global`. Starter templates: https://github.com/github/gitignore

## 2.8 Moving and deleting files

```bash
git mv old.js new.js     # rename + stage it
git rm file.js           # delete + stage it
git rm --cached file.js  # untrack but keep on disk
```

Git doesn't store renames explicitly; it detects them by similarity. A plain `mv` followed by `git add -A` works the same.

---

# Part 3: Undoing Things (Safety Net)

Knowing how to undo is what makes you fearless. First, identify **where** the mistake lives:

| Mistake is in... | Fix |
|---|---|
| Working directory (unstaged) | `git restore <file>` |
| Staging area | `git restore --staged <file>` |
| Last local commit | `git commit --amend` or `git reset` |
| A commit already pushed/shared | `git revert <sha>` |
| You "lost" something | `git reflog` |

## 3.1 `restore`

```bash
git restore file.js                  # DISCARD unstaged edits (cannot be undone!)
git restore --staged file.js         # unstage; edits stay in working dir
git restore --source=HEAD~2 file.js  # bring back file from 2 commits ago
git restore .                        # discard all unstaged changes (careful)
```

## 3.2 `reset`: move the branch pointer

`git reset <commit>` moves the current branch to `<commit>`. Three modes control what happens to the index and working directory:

| Mode | Branch pointer | Index (staging) | Working dir |
|---|---|---|---|
| `--soft` | moves | untouched | untouched |
| `--mixed` (default) | moves | reset | untouched |
| `--hard` | moves | reset | **reset (changes lost)** |

```bash
git reset --soft HEAD~1    # undo last commit, keep everything staged (redo the commit better)
git reset HEAD~1           # undo last commit, keep changes unstaged
git reset --hard HEAD~1    # undo last commit and DELETE its changes
git reset --hard origin/main  # make local branch identical to remote (discard local work)
```

`HEAD~1` = parent, `HEAD~3` = three back, `HEAD^2` = second parent of a merge commit.

**Never reset commits that others have already pulled.** It rewrites history under them.

## 3.3 `revert`: safe undo for shared history

```bash
git revert a3f9c2e              # creates a NEW commit that reverses a3f9c2e
git revert HEAD                 # revert the latest commit
git revert -m 1 <merge-sha>     # revert a merge commit (choose mainline parent)
git revert --no-commit A..B     # revert a range, then commit once
```

History is preserved, nothing is rewritten, so it's safe on `main`.

## 3.4 `stash`: shelve work in progress

```bash
git stash                      # stash tracked changes
git stash -u                   # include untracked files
git stash push -m "wip login"  # with a name
git stash list
git stash show -p stash@{0}    # see the diff
git stash pop                  # re-apply latest stash and drop it
git stash apply stash@{1}      # re-apply but keep it in the list
git stash drop stash@{0}
git stash branch new-branch    # create a branch from a stash
```

Use case: you're mid-feature and a production bug arrives. `git stash`, switch to `main`, fix, come back, `git stash pop`.

## 3.5 `reflog`: the "undo for your undo"

Git records every movement of HEAD locally for about 90 days.

```bash
git reflog
# a3f9c2e HEAD@{0}: reset: moving to HEAD~3
# 7be21d4 HEAD@{1}: commit: Add search feature
```

Recovering after a bad `reset --hard`:

```bash
git reflog                       # find the commit you want back
git reset --hard 7be21d4         # or: git switch -c rescued 7be21d4
```

If it was ever **committed**, it's almost always recoverable. If it was never committed or staged, it's usually gone. **Commit early, even messy WIP commits.**

---

# Part 4: Branching

## 4.1 Why branches?

A branch is a 41-byte file containing a commit hash, so creating one is instant. Branching lets you isolate each task (feature, fix, experiment) and keep `main` stable.

```
          D - E        (feature/login)
         /
A - B - C              (main)
```

## 4.2 Commands

```bash
git branch                    # list local branches (* = current)
git branch -a                 # include remote-tracking branches
git branch -vv                # with last commit + upstream tracking info
git branch feature/login      # create (stay here)
git switch feature/login      # move to it
git switch -c feature/login   # create + move (most common)
git switch -c hotfix main     # create from a specific starting point
git switch -                  # jump back to previous branch
git branch -m new-name        # rename current branch
git branch -d feature/login   # delete if fully merged
git branch -D feature/login   # force delete
git branch --merged           # branches already merged into current
git branch --no-merged
```

`git checkout` historically did both `switch` and `restore`. It still works; `switch`/`restore` are clearer.

**Naming conventions:** `feature/add-search`, `fix/login-null-error`, `chore/bump-deps`, `docs/api-readme`, `hotfix/payment-crash`. Optionally include ticket IDs: `feature/PROJ-123-add-search`.

## 4.3 Detached HEAD

`git switch --detach a3f9c2e` or `git checkout a3f9c2e` puts HEAD directly on a commit instead of a branch. You can look around safely. If you commit here and then switch away, those commits are orphaned. To keep them: `git switch -c new-branch`.

## 4.4 Merging

```bash
git switch main
git merge feature/login
```

### Fast-forward merge
If `main` has not advanced since you branched, Git just moves the pointer:

```
Before:  A - B - C (main)
                  \ D - E (feature)
After:   A - B - C - D - E (main, feature)
```

Force a merge commit anyway: `git merge --no-ff feature/login`. Only fast-forward allowed: `git merge --ff-only`.

### Three-way merge
If both branches advanced, Git finds the **merge base** (common ancestor) and combines both sets of changes, creating a **merge commit** with two parents:

```
A - B - C - F ------- M (main)
         \           /
          D - E -----
```

### Useful merge options
```bash
git merge --abort                 # cancel an in-progress merge with conflicts
git merge --squash feature        # combine all changes into the index; you make one commit
git merge -X theirs feature       # on conflicts, prefer incoming side (use sparingly)
```

## 4.5 Merge conflicts, in depth

### Why conflicts happen
Both branches changed the **same lines** of the same file, or one deleted a file the other modified. Git cannot decide what's correct, so it asks you.

### Anatomy
```
git merge feature/login
CONFLICT (content): Merge conflict in src/auth.js
Automatic merge failed; fix conflicts and then commit the result.
```

```js
function validate(user) {
<<<<<<< HEAD
  if (!user.email.includes("@")) throw new Error("bad email");
=======
  if (!isValidEmail(user.email)) throw new ValidationError("email");
>>>>>>> feature/login
  return true;
}
```

- `<<<<<<< HEAD` ... `=======`: **your current branch** (what you're merging *into*)
- `=======` ... `>>>>>>> feature/login`: **incoming branch**

### Resolution workflow

```bash
git status                  # lists "both modified" files (Unmerged paths)
# 1. open each file, decide the correct final code
# 2. remove ALL marker lines (<<<<<<<, =======, >>>>>>>)
# 3. RUN THE TESTS / BUILD
git add src/auth.js         # marking as resolved
git commit                  # completes the merge (message pre-filled)
```

Shortcuts:
```bash
git checkout --ours   file   # keep your side entirely  (during merge)
git checkout --theirs file   # keep incoming side entirely
git diff --name-only --diff-filter=U    # list unresolved files
git log --merge -p file      # commits from both sides touching the conflict
git mergetool                # launch a visual 3-way tool
git config --global merge.conflictStyle zdiff3   # also shows the common ancestor; makes conflicts far easier to understand
```

In VS Code: click "Accept Current / Incoming / Both" or use the 3-way merge editor.

### Conflict-resolution mindset
- Don't just pick a side: understand **what each side was trying to do**, then write code that achieves both.
- If you don't understand the other side's change, **ask the author** (use `git blame`/`git log`).
- Always test after resolving. A conflict-free but broken merge is the most common failure.
- `git merge --abort` is always available if you get lost.

### Reducing conflicts
- Short-lived branches (days, not weeks)
- Merge/rebase `main` into your branch frequently
- Avoid mass reformatting mixed with logic changes
- Agree on a formatter (Prettier, Black, gofmt) enforced by CI
- Split large files; communicate about who touches what

### Remembering resolutions: `rerere`
```bash
git config --global rerere.enabled true
```
Git remembers how you resolved a conflict and replays it automatically if the same conflict recurs (handy when rebasing long branches repeatedly).

---

# Part 5: Remotes

## 5.1 Concepts

A **remote** is a named URL to another copy of the repo. A **remote-tracking branch** (like `origin/main`) is your local, read-only bookmark of where that branch was the last time you talked to the remote. It updates only on `fetch`/`pull`/`push`.

```
Your machine                          GitHub
main            (local branch)        main
origin/main     (bookmark)  <-fetch-  main
```

## 5.2 Commands

```bash
git remote -v                              # list remotes
git remote add origin git@github.com:me/repo.git
git remote set-url origin <new-url>
git remote rename origin upstream
git remote remove origin
git remote show origin                     # details: tracked branches, push/pull config
```

### push
```bash
git push origin main
git push -u origin feature/login   # first push: set upstream (-u = --set-upstream)
git push                           # after upstream set
git push origin --delete old-branch
git push --tags                    # push all tags
git push --force-with-lease        # safe-ish force push (fails if someone else pushed)
```

### fetch
```bash
git fetch                  # update all remote-tracking branches; touches NO local branches/files
git fetch --prune          # also remove bookmarks of branches deleted on remote
git fetch origin pull/42/head:pr-42    # GitHub: grab a PR as a local branch
```
`fetch` is always safe. After it you can inspect before integrating:
```bash
git log main..origin/main --oneline    # what's new upstream
git diff main origin/main
```

### pull
```bash
git pull               # = git fetch + git merge origin/<current>
git pull --rebase      # = git fetch + git rebase origin/<current>
git pull --ff-only     # refuse if it would need a merge commit (very safe default)
```

Set a default so you aren't asked:
```bash
git config --global pull.rebase true      # linear history, many teams prefer
# or
git config --global pull.ff only          # fail loudly if histories diverged
```

### Tracking branches
```bash
git branch -vv                              # shows [origin/feature: ahead 2, behind 1]
git branch --set-upstream-to=origin/feature
git switch feature   # if only origin/feature exists, Git auto-creates a local tracking branch
```

## 5.3 Rejected pushes

```
! [rejected] main -> main (fetch first)
```
The remote has commits you don't. Fix:
```bash
git pull --rebase        # or git pull
# resolve conflicts if any
git push
```
Do **not** reflexively `--force`. Force pushing to a shared branch erases teammates' work.

Legit force push: after rebasing **your own** feature branch:
```bash
git push --force-with-lease
```
It refuses if the remote branch has commits you haven't seen.

## 5.4 Authentication

**SSH (recommended):**
```bash
ssh-keygen -t ed25519 -C "you@example.com"
cat ~/.ssh/id_ed25519.pub        # paste into GitHub > Settings > SSH keys
ssh -T git@github.com            # test
```
Use SSH URLs: `git@github.com:user/repo.git`.

**HTTPS:** use a Personal Access Token (not your password) or the GitHub CLI / Git Credential Manager.
```bash
gh auth login      # GitHub CLI handles it for you
```

## 5.5 Forks and upstream

Contributing to someone else's project:
1. **Fork** on GitHub (your own server-side copy).
2. `git clone` your fork. `origin` = your fork.
3. Add the original: `git remote add upstream https://github.com/original/repo.git`
4. Keep in sync:
```bash
git fetch upstream
git switch main
git merge upstream/main        # or rebase
git push origin main
```
5. Branch, commit, push to `origin`, open a PR **from your fork to upstream**.

---

# Part 6: Pull Requests

A PR is a GitHub/GitLab/Bitbucket feature (GitLab calls it a Merge Request). It says: "Here's a branch. Please review it and merge it into `main`."

## 6.1 Full lifecycle

1. **Branch:** `git switch -c feature/rate-limit`
2. **Commit** in small, logical pieces.
3. **Sync** with `main` before opening (pull/rebase).
4. **Push:** `git push -u origin feature/rate-limit`
5. **Open PR** (GitHub shows a banner, or `gh pr create`).
6. **CI runs** (tests, lint, build, security scan). Fix failures by pushing more commits; the PR auto-updates.
7. **Review:** teammates comment, request changes, or approve.
8. **Address feedback:** push fixes, reply to threads, resolve conversations.
9. **Merge** (when approved + CI green).
10. **Clean up:** delete the branch, update local `main`.

## 6.2 Writing a great PR

**Title:** specific, imperative. "Add rate limiting to login endpoint", not "updates".

**Description template:**
```markdown
## What
Adds Redis-backed rate limiting (5 attempts / IP / minute) to POST /login.

## Why
Prevents brute-force attacks. Closes #42.

## How
- New middleware `rateLimiter.js` using INCR + EXPIRE
- Returns 429 with Retry-After header

## Testing
- Unit tests for limiter
- Manual: 6 rapid requests -> 6th returns 429

## Screenshots / Notes
(if UI) / Needs REDIS_URL env var
```

`Closes #42` / `Fixes #42` in the description auto-closes the issue on merge.

**Size:** aim for < 400 changed lines. Big PRs get skimmed, not reviewed.
**Scope:** one purpose. Don't mix a refactor and a feature.
**Draft PRs:** open early as "Draft" to get feedback or show progress without requesting formal review.
**Self-review first:** read your own diff in the GitHub UI before tagging reviewers; you'll catch 30% of issues.

## 6.3 Reviewing well

Reviewer checklist:
- Does it do what the description claims? (pull the branch and run it for non-trivial changes)
- Correctness, edge cases, error handling
- Security (input validation, auth, secrets)
- Tests: are new behaviours covered?
- Readability and naming
- Performance, only where it matters

Comment etiquette:
- Ask questions instead of issuing orders: "What happens if `user` is null here?"
- Label severity: `nit:` (optional), `suggestion:`, `blocking:`
- Praise good work
- Use GitHub's "Suggest change" feature for small fixes the author can accept in one click

Review actions: **Comment** (no verdict), **Approve**, **Request changes** (blocks merging when branch protection requires it).

Authors: the review is about the code, not you. Respond to every comment (done / won't do because...). Push fixes as new commits during review (so reviewers can see what changed), squash at merge.

## 6.4 Merge strategies

| Strategy | What lands on `main` | Pros | Cons |
|---|---|---|---|
| **Merge commit** | all branch commits + a merge commit | full fidelity | noisy graph |
| **Squash and merge** | ONE new commit with all changes | clean, 1 commit per PR, easy revert | loses granular history (still visible on the PR page) |
| **Rebase and merge** | each commit replayed onto main, no merge commit | linear history, keeps commits | rewrites commit hashes; requires tidy commits |

Squash merge is the default at many companies because "wip", "fix typo", "address review" commits vanish and the PR title becomes the commit message. Edit that message before confirming. Easy revert: one PR = one commit = one `git revert`.

**After a squash merge**, `git branch -d` may refuse (Git can't see your commits in `main`'s history). Confirm the PR is merged, then use `-D`.

Cleanup:
```bash
git switch main
git pull
git branch -d feature/rate-limit      # or -D after squash
git fetch --prune                     # drop stale remote-tracking refs
```

## 6.5 Branch protection (what teams configure)

On `main`: require PR before merging, require N approvals, require status checks (CI) to pass, require branches to be up to date, block force pushes, require signed commits, require linear history, CODEOWNERS review for sensitive paths.

`CODEOWNERS` file example:
```
# .github/CODEOWNERS
*.sql           @db-team
/payments/      @alice @bob
```

## 6.6 GitHub CLI quickies

```bash
gh pr create --fill          # use commit info for title/body
gh pr list
gh pr checkout 42            # fetch and switch to PR #42
gh pr view --web
gh pr merge --squash --delete-branch
gh pr review --approve
gh issue create
```

---

# Part 7: Rebase

## 7.1 Merge vs rebase

Both integrate changes from one branch into another.

**Merge** preserves history as it happened, adding a merge commit.
**Rebase** *replays* your commits on top of another base, creating **new commits** (new hashes) and a straight line.

```
Before:
      D - E   (feature)
     /
A - B - C - F (main)

After  git switch feature && git rebase main:
A - B - C - F (main)
             \ D' - E'  (feature)

Then   git switch main && git merge feature   -> fast-forward, no merge commit
```

D' and E' contain the same changes as D and E but have different hashes because their parent changed.

## 7.2 Using it

```bash
git switch feature
git fetch origin
git rebase origin/main
```
Conflicts are resolved **commit by commit**:
```bash
# fix files
git add <files>
git rebase --continue
# or
git rebase --skip      # drop this commit
git rebase --abort     # return to pre-rebase state (always available)
```

## 7.3 The Golden Rule

> **Never rebase commits that others have based work on (shared/public history).**

Rebase rewrites history. If teammates pulled the old commits, their copies now diverge from yours, producing duplicate commits and painful conflicts. Rebase your **own unpushed or personal-branch** work; don't rebase `main` or shared branches.

## 7.4 Interactive rebase: edit history like a pro

```bash
git rebase -i HEAD~5          # edit the last 5 commits
git rebase -i main            # edit all commits since branching from main
git rebase -i --root          # everything
```

An editor opens listing commits oldest -> newest:

```
pick a1b2c3 Add login form
pick d4e5f6 wip
pick 7a8b9c Fix typo
pick 0d1e2f Add validation
```

Change the word at the start of each line:

| Command | Effect |
|---|---|
| `pick` | keep as is |
| `reword` | keep changes, edit message |
| `edit` | stop here so you can change the commit (split it, amend it) |
| `squash` | meld into previous commit, combine messages |
| `fixup` | meld into previous, discard this message |
| `drop` | delete the commit |
| (reorder lines) | reorder commits |

Example cleanup:
```
pick a1b2c3 Add login form
fixup d4e5f6 wip
fixup 7a8b9c Fix typo
pick 0d1e2f Add validation
```
Result: 2 clean commits.

### Autosquash workflow (very efficient)
```bash
git commit --fixup a1b2c3          # create a "fixup! Add login form" commit
git commit --fixup=amend:a1b2c3    # variant that also lets you edit message
git rebase -i --autosquash main    # auto-arranges fixups next to their targets
git config --global rebase.autoSquash true
```

### Splitting a commit
```bash
git rebase -i HEAD~3      # mark target commit as "edit"
git reset HEAD~1          # undo that commit, keep changes in working dir
git add -p; git commit    # make several smaller commits
git rebase --continue
```

### Rebase onto
Move a branch to a different base:
```bash
git rebase --onto main feature-a feature-b
# take commits of feature-b that aren't in feature-a, replay them onto main
```

## 7.5 Merge or rebase: practical guidance

- **Updating your feature branch with `main`:** rebase (if the branch is only yours) for a clean line; merge if shared.
- **Landing a feature into `main`:** follow team policy (usually squash merge via PR).
- **Public/shared branches:** merge only.
- Rebase before opening a PR to tidy commits; after review starts, prefer adding commits (and `--force-with-lease` only if reviewers are fine with it).

---

# Part 8: Cherry-pick, Tags, Worktrees, and More Tools

## 8.1 `cherry-pick`: copy specific commits

```bash
git cherry-pick a3f9c2e               # apply that commit's changes onto current branch
git cherry-pick A B C                 # multiple
git cherry-pick A..C                  # range (excludes A)
git cherry-pick -x a3f9c2e            # append "(cherry picked from ...)" to message
git cherry-pick --no-commit a3f9c2e   # apply without committing
git cherry-pick --continue / --abort
```
Use case: a bug fix on `main` must also go into `release/1.4`. Switch to the release branch and cherry-pick the fix commit.

## 8.2 Tags: marking releases

```bash
git tag                          # list
git tag v1.0.0                   # lightweight tag
git tag -a v1.0.0 -m "Release 1.0.0"   # annotated (preferred; has author/date/message)
git tag -a v1.0.0 a3f9c2e        # tag an older commit
git push origin v1.0.0
git push origin --tags
git tag -d v1.0.0                # delete locally
git push origin --delete v1.0.0  # delete remote
git describe --tags              # "v1.0.0-5-ga3f9c2e": 5 commits after v1.0.0
```
Follow **Semantic Versioning**: `MAJOR.MINOR.PATCH` (breaking.feature.fix).

## 8.3 `worktree`: multiple branches checked out at once

```bash
git worktree add ../hotfix-dir hotfix/payment    # new folder with that branch checked out
git worktree list
git worktree remove ../hotfix-dir
```
No more stashing to review a PR or fix a bug on another branch; each worktree has its own working directory but shares one `.git`.

## 8.4 `bisect`: binary-search for the commit that broke something

```bash
git bisect start
git bisect bad                   # current commit is broken
git bisect good v1.2.0           # this old one worked
# Git checks out a midpoint; you test, then:
git bisect good    # or
git bisect bad
# repeat ~log2(N) times -> Git names the first bad commit
git bisect reset                 # finish and return
```
Automate it:
```bash
git bisect start HEAD v1.2.0
git bisect run npm test          # script exit 0 = good, non-zero = bad
```
With 1,000 commits you find the culprit in ~10 steps.

## 8.5 `grep`, `shortlog`, `blame` toolkit

```bash
git grep "TODO"                  # search tracked files (fast)
git grep -n "validateUser" -- "*.js"
git shortlog -sn                 # commits per author
git log --stat --author="Kishan" --since="1 month ago"
git blame -w -C -C file.js       # ignore whitespace, detect code moved between files
git config blame.ignoreRevsFile .git-blame-ignore-revs   # skip mass-format commits in blame
```

## 8.6 Partial and sparse checkouts (huge repos)

```bash
git clone --filter=blob:none <url>        # "blobless" partial clone
git sparse-checkout init --cone
git sparse-checkout set apps/web libs/ui  # only materialize these folders
```

## 8.7 Submodules and subtrees (brief)

Submodules pin another repo at a specific commit inside yours:
```bash
git submodule add <url> vendor/lib
git clone --recurse-submodules <url>
git submodule update --init --recursive
```
They're powerful but fiddly. Prefer package managers when possible. `git subtree` is an alternative that copies the other repo's content into yours.

## 8.8 Aliases to speed you up

```bash
git config --global alias.st "status -sb"
git config --global alias.co "checkout"
git config --global alias.sw "switch"
git config --global alias.br "branch"
git config --global alias.cm "commit -m"
git config --global alias.last "log -1 HEAD --stat"
git config --global alias.unstage "restore --staged"
git config --global alias.lg "log --oneline --graph --all --decorate"
git config --global alias.amend "commit --amend --no-edit"
git config --global alias.undo "reset --soft HEAD~1"
```

---

# Part 9: Rewriting History and Recovery

## 9.1 Removing a secret/large file from all history

If you pushed `.env`:
1. **Rotate the credential immediately.** Assume it's public.
2. Then remove from history with **git-filter-repo** (modern replacement for `filter-branch`):
```bash
pip install git-filter-repo
git filter-repo --path .env --invert-paths
git push --force --all     # everyone must re-clone afterwards
```
Also consider BFG Repo-Cleaner. Coordinate with the team; this rewrites every subsequent commit hash.

Prevent it: secret scanners (`gitleaks`, GitHub push protection), pre-commit hooks, `.gitignore` from day 1.

## 9.2 Recovery scenarios

| Situation | Solution |
|---|---|
| Committed to wrong branch | `git switch correct-branch && git cherry-pick <sha>`, then on the wrong branch `git reset --hard HEAD~1` |
| Bad commit message (unpushed) | `git commit --amend` |
| Forgot a file in last commit | `git add file && git commit --amend --no-edit` |
| Accidentally `reset --hard` | `git reflog` -> `git reset --hard <sha>` |
| Deleted a branch | `git reflog` -> `git switch -c name <sha>` |
| Merge went wrong (not committed) | `git merge --abort` |
| Merge went wrong (committed, unpushed) | `git reset --hard ORIG_HEAD` |
| Bad commit already pushed to main | `git revert <sha>` |
| Pushed secret | Rotate + filter-repo |
| Need old version of a file | `git restore --source=<sha> path` |
| Dangling/lost commits | `git fsck --lost-found` |
| Detached HEAD with new commits | `git switch -c rescue` |
| Pulled and made a mess | `git reflog`, reset to the pre-pull entry (`ORIG_HEAD` helps) |

`ORIG_HEAD` is set by dangerous operations (merge, rebase, reset, pull) and points to where HEAD was before.

## 9.3 Garbage collection

Unreachable objects are pruned eventually (`git gc`), typically after ~2 weeks, reflog entries after ~90 days. `git gc --prune=now` aggressively deletes. Normally let Git handle it.

---

# Part 10: Debugging with Git

```bash
git log -S"calculateTax" --oneline        # when did this string appear/disappear?
git log -L :calculateTax:src/tax.js       # full history of one function
git log --follow -p -- path/to/file       # file history incl. renames
git blame -L 40,60 file.js
git bisect ...                            # Part 8.4
git diff v1.0..v2.0 --stat                # what changed between releases
git show <sha> --stat
git shortlog -sn v1.0..v2.0               # who contributed to a release
git range-diff main old-feature new-feature   # compare two versions of a rebased branch
git reflog show feature                   # history of a specific branch pointer
```

---

# Part 11: Git Internals (Hero Level)

Understanding internals removes the "magic." Everything lives in `.git/`:

```
.git/
  HEAD          -> "ref: refs/heads/main"
  config        -> repo-level config
  index         -> the staging area (binary)
  objects/      -> the database of all content
  refs/
    heads/      -> local branches (files containing a SHA)
    remotes/    -> remote-tracking branches
    tags/
  hooks/        -> scripts triggered by Git events
  logs/         -> reflog
```

## 11.1 Four object types

All stored in `objects/`, addressed by the SHA-1 of their content (content-addressable storage):

1. **blob**: the contents of a file (no name, no permissions).
2. **tree**: a directory listing: names -> blob/tree hashes (+ file modes).
3. **commit**: points to a root tree + parent commit(s) + author + committer + message.
4. **tag** (annotated): points to a commit + tagger + message.

```
commit a3f9
  |-- tree 91c2          (snapshot root)
  |     |-- blob 5d1e  README.md
  |     |-- tree 7be2  src/
  |           |-- blob c04a  app.js
  |-- parent 8f21 (previous commit)
  |-- author, message
```

Identical content = identical hash = stored once. That's why branching and copying are cheap, and why unchanged files in a new commit reuse existing blobs.

## 11.2 Explore it yourself

```bash
echo "hello" | git hash-object --stdin          # compute a blob hash
git cat-file -t HEAD                            # type of object: commit
git cat-file -p HEAD                            # pretty-print a commit
git cat-file -p HEAD^{tree}                     # its root tree
git ls-tree -r HEAD                             # full file listing of a commit
git ls-files -s                                 # the index contents
cat .git/HEAD
cat .git/refs/heads/main                        # a branch is literally a file with a SHA
git count-objects -vH
```

Build a commit by hand (plumbing vs porcelain commands):
```bash
echo "v1" > f.txt
git hash-object -w f.txt                   # write blob
git update-index --add --cacheinfo 100644,<blob-sha>,f.txt
git write-tree                             # create tree from index
git commit-tree <tree-sha> -m "manual"     # create commit object
git update-ref refs/heads/main <commit-sha>
```
"Porcelain" = friendly commands (`commit`, `add`). "Plumbing" = low-level ones (`hash-object`, `cat-file`).

## 11.3 Packfiles

Loose objects are zlib-compressed individually. `git gc` packs them into **packfiles** that use **delta compression** (store similar objects as differences). So although conceptually every commit is a full snapshot, storage is efficient.

## 11.4 Refs, revision syntax

- `HEAD`, `main`, `origin/main`, `v1.0` are **refs** (names pointing to SHAs).
- `HEAD~1`: first-parent ancestor, `HEAD~3`: 3 back along first parents.
- `HEAD^`: first parent, `HEAD^2`: second parent (merge).
- `main@{yesterday}`, `HEAD@{5}`: reflog-based.
- `a..b`: commits reachable from b not from a. `a...b`: symmetric difference.
- `:/fix login`: most recent commit whose message matches.

## 11.5 How merge picks a base

`git merge-base main feature` shows the best common ancestor. A three-way merge compares **base vs ours vs theirs**: if only one side changed a hunk, take that change; if both changed it differently -> conflict.

## 11.6 Why rebase "changes history"

Commits are immutable (the hash includes the parent). "Rewriting" always means creating **new** commits and moving the branch label. The old ones linger in the reflog until garbage-collected, which is why you can recover.

---

# Part 12: Hooks and Automation

Hooks are scripts in `.git/hooks/` that run on events. Common ones:

| Hook | Runs | Typical use |
|---|---|---|
| `pre-commit` | before commit is created | lint, format, secret scan |
| `commit-msg` | after message written | enforce Conventional Commits |
| `pre-push` | before push | run tests |
| `post-merge` | after merge/pull | reinstall deps |

Example `.git/hooks/pre-commit` (make executable: `chmod +x`):
```bash
#!/bin/sh
npm run lint || exit 1
```
Skip once (use sparingly): `git commit --no-verify`.

`.git/hooks` isn't versioned, so teams use tools that install hooks for everyone:
- **Husky** + **lint-staged** (JS projects)
- **pre-commit** (Python framework, any language)
- **Lefthook**

Example with Husky:
```bash
npm i -D husky lint-staged
npx husky init
echo "npx lint-staged" > .husky/pre-commit
```
```json
"lint-staged": { "*.{js,ts}": ["eslint --fix", "prettier --write"] }
```

### Commit message linting
`commitlint` + Conventional Commits config in `commit-msg` hook.

### CI (GitHub Actions) basics
`.github/workflows/ci.yml`:
```yaml
name: CI
on:
  pull_request:
  push:
    branches: [main]
jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-node@v4
        with: { node-version: 20, cache: npm }
      - run: npm ci
      - run: npm run lint
      - run: npm test
```
CI runs on every PR; branch protection requires it to pass before merging.

---

# Part 13: Collaboration Workflows

## 13.1 Common branching strategies

**GitHub Flow** (simple, most web teams)
- `main` is always deployable.
- Branch -> commit -> PR -> review -> merge -> deploy.

**Trunk-based development** (high-velocity teams, Google-style)
- Very short-lived branches (hours to 1-2 days), merge to `main` constantly.
- Unfinished work hidden behind **feature flags**.
- Requires strong CI and testing.

**Git Flow** (scheduled releases, e.g. mobile/desktop)
- Long-lived `main` + `develop`; `feature/*`, `release/*`, `hotfix/*` branches.
- More ceremony; fading in popularity for web.

**Forking workflow** (open source): fork, branch, PR from fork.

Pick the simplest one that fits your release cadence. Most teams: GitHub Flow or trunk-based.

## 13.2 Conventional Commits

```
<type>(optional scope): <description>

[optional body]

[optional footer(s)]
```
Types: `feat`, `fix`, `docs`, `style`, `refactor`, `perf`, `test`, `build`, `ci`, `chore`, `revert`.
```
feat(auth): add refresh token rotation
fix(api): handle null user in /profile
feat!: drop Node 16 support

BREAKING CHANGE: requires Node 18+
```
Benefits: automated changelogs and version bumps (semantic-release, release-please).

## 13.3 Commit message craft

```
Add rate limiting to login endpoint

Limit to 5 attempts per IP per minute using Redis INCR+EXPIRE.
Brute-force protection requested in #42. Returns 429 with a
Retry-After header.

Closes #42
```
- Subject: imperative, <= 50 chars, no trailing period. Test: "If applied, this commit will ___."
- Blank line, then body wrapped at ~72 chars explaining **why** and trade-offs.
- Bad: `fix`, `update`, `stuff`, `asdf`, `final2`.

## 13.4 Atomic commits

One logical change per commit; the codebase should build and tests should pass at each commit. Benefits: precise `revert`, effective `bisect`, easier review, readable `blame`.

Test: can you describe it in one sentence without "and"? If not, split it with `git add -p`.

## 13.5 Daily team routine

```bash
git switch main && git pull --ff-only     # start fresh
git switch -c feature/xyz
# work, commit small & often
git fetch origin
git rebase origin/main                    # keep up to date (own branch)
git push -u origin feature/xyz
gh pr create --fill
# review cycle...
# after merge:
git switch main && git pull --ff-only
git branch -D feature/xyz
git fetch --prune
```

## 13.6 Monorepos and large files

- Large binaries: use **Git LFS** (`git lfs track "*.psd"`), never commit videos/datasets raw.
- Monorepo tooling: sparse checkout, partial clone, CODEOWNERS, path-filtered CI.

## 13.7 Signing commits

```bash
git config --global gpg.format ssh
git config --global user.signingkey ~/.ssh/id_ed25519.pub
git config --global commit.gpgsign true
```
GitHub shows a "Verified" badge. Some orgs require it.

## 13.8 Security checklist

- Never commit secrets; use env vars/secret managers.
- Add `.env` to `.gitignore` on day one; commit `.env.example`.
- Enable GitHub secret scanning + push protection.
- Review third-party hooks/scripts before running (`.git/hooks`, `postinstall`).
- Use SSH keys with passphrases; rotate leaked tokens immediately.
- Protect `main`.

---

# Part 14: Hero Habits (Condensed)

1. `git status` and `git diff --staged` before every commit.
2. Commit early, commit often locally; clean up with interactive rebase before the PR.
3. One logical change per commit; one purpose per PR.
4. Write messages for the person debugging at 2 AM (including future you).
5. Pull/rebase from `main` often; keep branches short-lived.
6. Never force-push shared branches; use `--force-with-lease` on personal ones.
7. Never commit secrets, `node_modules`, or build artifacts.
8. Know your escape hatches: `merge --abort`, `rebase --abort`, `reflog`, `revert`.
9. Automate: hooks, CI, formatters, commit linting.
10. Read others' PRs; reviewing is the fastest way to level up.

---

# Part 15: Complete Hands-on Course

Do these in order; each builds a skill.

### Lab 1: Basics (20 min)
```bash
mkdir lab1 && cd lab1 && git init
echo "node_modules/" > .gitignore
echo "# Lab 1" > README.md
git status
git add . && git commit -m "Initial commit"
echo "line 2" >> README.md
git diff
git add -p
git commit -m "Expand README"
git log --oneline --graph
```
Goal: see each state change in `git status`.

### Lab 2: Undo everything (20 min)
1. Edit a file, `git restore` it.
2. Stage a file, `git restore --staged` it.
3. Commit with a typo, `git commit --amend`.
4. Make 3 commits, `git reset --soft HEAD~3`, recommit as one.
5. `git reset --hard HEAD~1` then recover with `git reflog`.
6. `git revert` a commit.

### Lab 3: Branches and conflicts (30 min)
```bash
git switch -c feature/a
echo "from A" > app.txt && git add . && git commit -m "A"
git switch main
echo "from main" > app.txt && git add . && git commit -m "main"
git merge feature/a      # conflict
```
Resolve it manually, then in VS Code, then with `git mergetool`. Repeat using `git merge --abort`. Turn on `merge.conflictStyle zdiff3` and notice the difference.

### Lab 4: Remotes (30 min)
1. Create an empty GitHub repo; `git remote add origin ...; git push -u origin main`.
2. Clone it a second time into `lab4-copy/` (simulating a teammate).
3. Commit in each copy to the same file; push from one; try to push from the other (rejected).
4. `git pull --rebase`, resolve conflict, push.

### Lab 5: PR flow (30 min)
Create a branch, push it, open a PR to your own repo, leave a review comment on your own PR, push a fix, squash-merge, delete branch, sync local `main`.

### Lab 6: Rebase and history cleanup (40 min)
1. Make 6 messy commits (`wip`, `typo`, ...) on a branch.
2. `git rebase -i main`: squash/fixup/reword/reorder to 2 clean commits.
3. Try `git commit --fixup` + `--autosquash`.
4. Split one commit using `edit`.

### Lab 7: Detective work (30 min)
1. Introduce a bug across 20 commits (script it).
2. Find it with `git bisect` (manual, then `bisect run`).
3. Use `git log -S`, `git blame -L`, and `git log -L :func:file`.

### Lab 8: Internals (30 min)
Use `cat-file`, `ls-tree`, `hash-object`; make a commit via plumbing; inspect `.git/refs/heads/main` and `.git/HEAD`; run `git gc` and compare `count-objects`.

### Lab 9: Team simulation (60 min)
With a friend (or two clones): protect `main`, add CI workflow, work on separate features, have a deliberate conflict, review each other's PRs, squash merge, tag `v1.0.0`, cherry-pick a hotfix into a release branch.

### Lab 10: Automation (30 min)
Add Husky + lint-staged + commitlint to a Node project; try to commit a badly formatted file and a bad message; watch them get blocked.

---

# Part 16: Troubleshooting FAQ

**"fatal: not a git repository"**: you're not inside a repo folder (or `.git` was deleted). `git init` or `cd` correctly.

**"Your branch is ahead of 'origin/main' by 2 commits"**: you have local commits not pushed. `git push`.

**"Your branch and 'origin/main' have diverged"**: both sides have new commits. `git pull --rebase` (or `merge`).

**"error: failed to push some refs ... (fetch first)"**: pull first, then push.

**"You have unmerged paths"**: unresolved conflicts. Fix files, `git add`, then continue/commit.

**"Please commit your changes or stash them before you switch branches"**: `git stash` (or commit), then switch.

**"fatal: refusing to merge unrelated histories"**: two repos with no common ancestor. Use `--allow-unrelated-histories` only if you really intend it.

**"detached HEAD"**: you checked out a commit/tag. `git switch -c name` to keep work, or `git switch main` to leave.

**"Permission denied (publickey)"**: SSH key not added to GitHub/agent. `ssh-add ~/.ssh/id_ed25519`, check `ssh -T git@github.com`.

**"remote: Support for password authentication was removed"**: use a token or SSH.

**Files showing as modified but you changed nothing**: line endings (CRLF/LF) or file mode. Set `core.autocrlf`, add `.gitattributes`: `* text=auto`; `git config core.fileMode false` for permission noise.

**`.gitignore` not working**: file is already tracked. `git rm --cached <file>`.

**Case-only rename not detected (Windows/macOS)**: `git mv File.js file.js` or `git config core.ignorecase false`.

**Repo is huge/slow**: `git gc`, check for big files with `git rev-list --objects --all | git cat-file --batch-check='%(objecttype) %(objectname) %(objectsize) %(rest)' | sort -k3 -n -r | head`, move binaries to LFS.

**Accidentally committed to `main` (unpushed)**:
```bash
git branch feature/oops        # keep the commits on a new branch
git reset --hard origin/main   # reset main
git switch feature/oops
```

**Want to undo a merge that's already pushed**: `git revert -m 1 <merge-sha>`.

---

# Part 17: Cheat Sheets

## Daily commands

| Goal | Command |
|---|---|
| Status (short) | `git status -sb` |
| Stage all / hunks | `git add -A` / `git add -p` |
| Commit | `git commit -m "msg"` |
| Amend last | `git commit --amend` |
| Unstaged / staged diff | `git diff` / `git diff --staged` |
| Graph log | `git log --oneline --graph --all` |
| New branch | `git switch -c name` |
| Merge | `git merge name` |
| Abort merge/rebase | `git merge --abort` / `git rebase --abort` |
| Fetch / pull / push | `git fetch` / `git pull --ff-only` / `git push` |
| First push of branch | `git push -u origin name` |
| Rebase on main | `git rebase origin/main` |
| Interactive rebase | `git rebase -i HEAD~N` |
| Stash / restore | `git stash -u` / `git stash pop` |
| Cherry-pick | `git cherry-pick <sha>` |
| Tag release | `git tag -a v1.0.0 -m "msg" && git push --tags` |

## Undo matrix

| Want to... | Command |
|---|---|
| Discard unstaged edits | `git restore f` |
| Unstage | `git restore --staged f` |
| Undo last commit, keep staged | `git reset --soft HEAD~1` |
| Undo last commit, keep unstaged | `git reset HEAD~1` |
| Nuke last commit and changes | `git reset --hard HEAD~1` |
| Safely undo shared commit | `git revert <sha>` |
| Get old file version | `git restore --source=<sha> f` |
| Recover "lost" commit | `git reflog` then `git reset --hard <sha>` |
| Match remote exactly | `git fetch && git reset --hard origin/main` |
| Delete untracked files | `git clean -fd` (try `-n` first) |

## Reset modes

```
--soft   : move branch only
--mixed  : move branch + reset index         (default)
--hard   : move branch + reset index + working dir
```

## Merge strategies on GitHub

```
Merge commit : keep everything + merge commit
Squash       : 1 commit per PR (clean main)
Rebase       : replay commits, linear, no merge commit
```

## Revision syntax

```
HEAD~2   2 commits back        HEAD^2   2nd parent (merge)
a..b     in b, not in a        a...b    in either, not both
@{u}     upstream of branch    @{-1}    previously checked-out branch
main@{yesterday}   reflog time travel
```

---

# Part 18: Learning Path Beyond This Guide

1. **Pro Git book** (free): https://git-scm.com/book, especially chapters 1-3, 7, 10.
2. **Learn Git Branching** (interactive visual game): https://learngitbranching.js.org
3. **Oh Shit, Git!?!**: practical recovery recipes: https://ohshitgit.com
4. **Git man pages**: `git help <command>` / `git <command> --help`.
5. Read real PRs in open-source projects you use; contribute a docs fix to learn the fork + PR flow.
6. Build "mini-git" in your favorite language (hash objects, trees, commits); the best way to truly own the internals.

---

## Final self-check: you're a Git hero when you can...

- [ ] Explain commits, branches, HEAD, index, and remotes using the snapshot/pointer model
- [ ] Craft clean commits with `add -p` and write meaningful messages
- [ ] Resolve any merge conflict calmly and test the result
- [ ] Choose between merge, rebase, squash, and revert, and say why
- [ ] Clean a branch with interactive rebase before a PR
- [ ] Recover from `reset --hard`, deleted branches, and bad merges using `reflog`
- [ ] Find the commit that introduced a bug with `bisect`, `log -S`, and `blame`
- [ ] Run a full PR cycle with review, CI, protected `main`, and squash merge
- [ ] Keep secrets out of history, and know what to do if one leaks
- [ ] Peek inside `.git/` and explain blobs, trees, commits, and refs

Practice beats reading. Do the labs, break things on purpose, and recover them.
