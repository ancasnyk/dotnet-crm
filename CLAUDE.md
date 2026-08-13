# CLAUDE.md

Project instructions for Claude Code in this repository.

## Committing

Always run the `mcp__Snyk__snyk_secret_scan` tool before any commit, and resolve
anything it reports before staging the commit.

There is no pre-commit hook enforcing this, so the scan is the only thing
standing between a hardcoded secret and a commit. Do not skip it.
