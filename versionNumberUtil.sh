#!/bin/bash

# Specify the path to the .csproj file
csproj_file="MazaheriLinkedList/MazaheriLinkedList.csproj"

# Check if the file exists
if [ ! -f "$csproj_file" ]; then
  echo "The .csproj file '$csproj_file' does not exist."
  exit 1
fi

# Get the current version using basic regular expressions
current_version=$(grep '<Version>' "$csproj_file" | sed -n 's/.*<Version>\(.*\)<\/Version>.*/\1/p')

if [ -z "$current_version" ]; then
  echo "No <Version> tag found in '$csproj_file'. Please make sure the file is formatted correctly."
  exit 1
fi

# Extract major, minor, and patch version components
IFS="." read -r major minor patch <<< "$current_version"

# Check if the patch version is greater than or equal to 10
if [ "$patch" -ge 10 ]; then
  # Increment the minor version
  minor=$((minor + 1))
  # Reset the patch version to 0
  patch=0
else
  # Increment the patch version
  patch=$((patch + 1))
fi

# Generate the new version
new_version="$major.$minor.$patch"

# Replace the old version with the new version
sed "s|<Version>$current_version</Version>|<Version>$new_version</Version>|g" "$csproj_file" > "$csproj_file".tmp 
mv "$csproj_file".tmp "$csproj_file"

echo "Updated version from $current_version to $new_version in $csproj_file."