import re

def natural_key(string):
    # Splits the string into digit and non-digit parts, handling negative numbers
    return [
        int(text) if re.fullmatch(r'-?\d+', text) else text.lower()
        for text in re.split(r'(-?\d+)', string)
    ]

def sort_file_strings(input_file="stuff.log", output_file="stuff.log", log_file="duplicates.log"):
    """
    Reads strings from an input file, sorts them in natural (human-friendly, numeric-aware) order,
    removes duplicates, logs any duplicates found, and writes the result to an output file.
    By default, both input and output files are 'stuff.log'. Duplicates are logged to 'duplicates.log'.
    Made with KI because I CAN
    """
    with open(input_file, 'r', encoding='utf-8') as f:
        lines = [line.rstrip('\n') for line in f]
    seen = set()
    sorted_lines = []
    duplicates = []
    for line in sorted(lines, key=natural_key):
        if line in seen:
            duplicates.append(line)
        else:
            seen.add(line)
            sorted_lines.append(line)
    with open(output_file, 'w', encoding='utf-8') as f:
        for line in sorted_lines:
            f.write(line + '\n')
    if duplicates:
        with open(log_file, 'w', encoding='utf-8') as logf:
            for dup in duplicates:
                logf.write(dup + '\n')

if __name__ == '__main__':
    import sys
    # Usage: python sort_strings.py [input_file] [output_file] [log_file]
    if len(sys.argv) == 1:
        sort_file_strings()
    elif len(sys.argv) == 2:
        sort_file_strings(sys.argv[1], sys.argv[1])
    elif len(sys.argv) == 3:
        sort_file_strings(sys.argv[1], sys.argv[2])
    elif len(sys.argv) == 4:
        sort_file_strings(sys.argv[1], sys.argv[2], sys.argv[3])
    else:
        print("Usage: python sort_strings.py [input_file] [output_file] [log_file]")