#import "Exit.h"

static NSString * ImgExit = @"exit.png";

@implementation Exit

@synthesize nextLevel;

+ (id) exitWithOrigin:(CGPoint)Origin
{
	return [[[self alloc] initWithOrigin:Origin] autorelease];
}

- (id) initWithOrigin:(CGPoint)Origin
{
	if ((self = [super initWithX:Origin.x y:Origin.y graphic:nil])) {
		
		[self loadGraphicWithParam1:ImgExit param2:YES param3:NO param4:66 param5:110];
        
        self.width=44;

        self.offset=CGPointMake(11, 0);
        
        
        if ((FlxG.level>=1 && FlxG.level <= 12) ||  FlxG.level==37 || FlxG.level==38 || FlxG.level==39 || FlxG.level==40 ) {
            [self addAnimationWithParam1:@"open" param2:[NSMutableArray intArrayWithSize:10 ints:0,0,0,1,0,0,1,0,1,0] param3:12]; 
            [self addAnimationWithParam1:@"closed" param2:[NSMutableArray intArrayWithSize:10 ints:2,2,2,3,2,2,3,2,3,2] param3:12]; 
        }
        else if ((FlxG.level>=13 && FlxG.level <= 24) ||  FlxG.level==41 || FlxG.level==42 || FlxG.level==43 || FlxG.level==44  ) {
            [self addAnimationWithParam1:@"open" param2:[NSMutableArray intArrayWithSize:10 ints:4,4,4,5,4,4,5,4,5,4] param3:12]; 
            [self addAnimationWithParam1:@"closed" param2:[NSMutableArray intArrayWithSize:10 ints:6,6,6,7,6,6,7,6,7,6] param3:12];  
        }        
        else if ((FlxG.level>=25 && FlxG.level <= 36) ||  FlxG.level==45 || FlxG.level==46 || FlxG.level==47 || FlxG.level==48 ) {
            [self addAnimationWithParam1:@"open" param2:[NSMutableArray intArrayWithSize:10 ints:8,8,8,9,8,8,9,8,9,8] param3:12]; 
            [self addAnimationWithParam1:@"closed" param2:[NSMutableArray intArrayWithSize:10 ints:10,10,10,11,10,10,11,10,11,10] param3:12]; 
        }
        else  {
            [self addAnimationWithParam1:@"open" param2:[NSMutableArray intArrayWithSize:10 ints:0,0,0,1,0,0,1,0,1,0] param3:12]; 
            [self addAnimationWithParam1:@"closed" param2:[NSMutableArray intArrayWithSize:10 ints:2,2,2,3,2,2,3,2,3,2] param3:12]; 
        }
        
        [self play:@"closed"];
        
        nextLevel=0;
        
	}
	
	return self;	
}

- (void) dealloc
{
    
	[super dealloc];
}

- (void) update
{   
	
	[super update];
	
}


@end
