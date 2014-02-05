import shutil
	
		
__characters = ['artist', 'assassin', 'automaton', 'bat', 'blight', 'bloatedzombie', 'bogbeast', 'bombling', 'centaur', 'chicken', 'chimaera', 'corsair', 'cow', 'cyclops', 'deathclaw', 'deer', 'devil', 'djinn', 'drone', 'druid', 'dwarf', 'embersteed', 'executor', 'feline', 'floatingeye', 'fungant', 'gelatine', 'gloom', 'glutton', 'goblin', 'golem', 'gorgon', 'gourmet', 'grimwarrior', 'grizzly', 'harvester', 'horse', 'ifrit', 'imp', 'kerberos', 'lich', 'lion', 'marksman', 'mechanic', 'mephisto', 'mistress', 'merchant', 'mermaid', 'mimick', 'monk', 'mummy', 'nightmare', 'nymph', 'ogre', 'paladin', 'phantom', 'priest', 'prism', 'rat', 'savage', 'seraphine', 'sheep', 'skeleton', 'snake', 'soldier', 'sphinx', 'spider', 'succubus', 'tauro', 'toad', 'tormentor', 'treant', 'troll', 'unicorn', 'vampire', 'warlock', 'willowisp', 'wizard', 'wolf', 'zinger', 'zombie']

for cha in __characters:
	print "if (command == \"" + cha + "\")"
	print "{"
	print "    "+cha+".startPlayingBack();"
	print "}"
			

'''
for cha in __characters:
	print"<EntityDefinition Name=\"" + cha +"\" Limit=\"-1\" ResizableX=\"false\" ResizableY=\"false\" Rotatable=\"false\" RotateIncrement=\"15\">"
	print"  <Size>"
	print"    <Width>16</Width>"
	print"    <Height>16</Height>"
	print"  </Size>"
	print"  <Origin>"
	print"    <X>0</X>"
	print"    <Y>0</Y>"
	print"  </Origin>"
	print"  <ImageDefinition DrawMode=\"Image\" ImagePath=\"characters\\"+ cha +".png\" Tiled=\"false\">"
	print"    <RectColor A=\"255\" R=\"255\" G=\"0\" B=\"0\" />"
	print"  </ImageDefinition>"
	print"  <ValueDefinitions>"
	print"    <ValueDefinition xsi:type=\"BoolValueDefinition\" Name=\"flip\" Default=\"false\" />"
	print"  </ValueDefinitions>"
	print"  <NodesDefinition Enabled=\"false\" Limit=\"-1\" DrawMode=\"None\" Ghost=\"false\" />"
	print"</EntityDefinition>"
'''