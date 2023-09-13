import os

# Path to the "client" folder
folder_path = 'client'

# Recursive function to list files in subfolders
def list_files(directory):
    file_names = []
    for root, _, files in os.walk(directory):
        for file_name in files:
            full_path = os.path.join(root, file_name)
            file_names.append(full_path[len(directory)+1:])
    return file_names

# Check if the path is a valid folder
if os.path.isdir(folder_path):
    # List all files in the folder and subfolders
    file_names = list_files(folder_path)

    # Format file names using forward slashes "/"
    formatted_file_names = [file_name.replace('\\', '/') for file_name in file_names]

    # Save file names to the "index.txt" file
    with open('files_list.txt', 'w') as index_file:
        for file_name in formatted_file_names:
            if file_name.strip():
                index_file.write(file_name + '\n')
    print('The file names have been saved in the file "files_list.txt".')
else:
    print(f'The folder "{folder_path}" does not exist.')
