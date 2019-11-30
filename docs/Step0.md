# Step 0: Setup

## Clone the Repository

Open a command prompt and change to a working directory of your choice.
On Windows, that might look like this:

```shell
cd C:\Dev\
```

Clone the repository into a child folder.

```shell
git clone https://github.com/solarwinds/mlnet-workshop.git
```

Change directory into the repository folder.

```shell
cd mlnet-workshop
```

## Check Out the Branch for Step 0

Wait, we're looking ahead!  Let's back up to a good starting point.

```shell
git checkout steps/step-0
```

If you get hopelessly lost during the workshop, just clean up the mess and check out the branch for the step you'd like to be on.
For example, if you'd like to move to the end of step 5 (ready to start step 6), you would do this.

```shell
git clean -fxd
git checkout steps/step-5
```

## Verify the .NET Core Version

Make sure you have .NET Core 3.0 or later installed.

```shell
dotnet --version
```

## Next

Go to [Step 1: Create a Project](./Step1.md).
