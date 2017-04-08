original = open('areaZnovember4.obj', 'r')
corrected = open('areaZnov4_corrected.obj', 'w')
for line in original:
    words = line.split()
    if words[0] == 'v': 
        words[1] = str(float(words[1]) - 734500)
        words[2] = str(float(words[2]) - 3396000)
        line = words[0] + ' ' + words[1] + ' ' + words[2] + ' ' + words[3] + '\n'
    corrected.write(line)
original.close()
corrected.close()
    
    
    