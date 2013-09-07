x = raw_input('What is this new variable called?:')
type = raw_input('What type of variable?:')

print "// Header - FlxG.h"
print "+ (void) set" + x[0].upper() + x[1:10000] + ":" + "(" + type + ") " + x + ";"
print "+ (" + type + ") " + x + ";"

print "//Implementation - FlxG.m"
print "static " + type + " " + x + ";"

print "//Implementation - FlxG.m - further down"
print "+ (void) set" + x[0].upper() + x[1:10000]+":("+type+")new"+x[0].upper() + x[1:10000]+"; { "+x+" = new" + x[0].upper() + x[1:10000]+"; }"
print "+ ("+type+") "+x+"; { return "+x+"; }"
