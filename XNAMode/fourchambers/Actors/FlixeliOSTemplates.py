'''

'''
import shutil


class Templates:
	"""

	"""
	def __init__(self):
		self.__files = ['templateActor.cs']
		
		
		
		# ---- USER INPUT ----
		
					
		self._classWithUpper = ''
		self._classWithLower = ''

	# ---- GO!
	
	
	def _replace_parts(self, replace=''):
		newline =''
		newline = replace.replace('__ClassNameWithUppercase__', self._classWithUpper)		
		newline = newline.replace('__ClassNameWithLowercase__', self._classWithLower)
		return newline
	
	
	def _create_files(self, userInput=False, className=''):
		
		if userInput:
			_class = raw_input("Class name: ")
		else:
			_class=className
				
		self._classWithUpper = _class[0].upper()+_class[1:1000]
		self._classWithLower = _class[0].lower()+_class[1:1000]
		
		
		for file in self.__files:
		
			f = open(file, 'r')
		
			o = 'output/'+self._classWithUpper + '.' + file[-2] + file[-1] 
		
			output = open(o, 'w')
		
		
		
			f2 = f.readlines()
			for i in f2:
				newline = ''
				newline =self._replace_parts(replace=i)
				output.write(newline)
			
			
			f.close()
			output.close()
			
		
		
		#copy template png sprite
		#shutil.copyfile('__FlxSpriteTemplate50x50__.png', 'output/'+_class+'.png')
	
		
__characters = ['artist', 'assassin', 'automaton', 'bat', 'blight', 'bloatedzombie', 'bogbeast', 'bombling', 'centaur', 'chicken', 'chimaera', 'corsair', 'cow', 'cyclops', 'deathclaw', 'deer', 'devil', 'djinn', 'drone', 'druid', 'dwarf', 'embersteed', 'executor', 'feline', 'floatingeye', 'fungant', 'gelatine', 'gloom', 'glutton', 'goblin', 'golem', 'gorgon', 'gourmet', 'grimwarrior', 'grizzly', 'harvester', 'horse', 'ifrit', 'imp', 'kerberos', 'lich', 'lion', 'marksman', 'mechanic', 'mephisto', 'merchant', 'mermaid', 'mimick', 'monk', 'mummy', 'nightmare', 'nymph', 'ogre', 'paladin', 'phantom', 'priest', 'prism', 'rat', 'savage', 'seraphine', 'sheep', 'skeleton', 'snake', 'soldier', 'sphinx', 'spider', 'succubus', 'tauro', 'toad', 'tormentor', 'treant', 'troll', 'unicorn', 'vampire', 'warlock', 'willowisp', 'wizard', 'wolf', 'zinger', 'zombie']

for cha in __characters:
	Templates()._create_files(userInput=False, className=cha)
	
	
print 'Complete'