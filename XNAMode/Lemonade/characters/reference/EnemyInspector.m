
#import "EnemyInspector.h"

#define ORIGINAL_HEALTH 5
#define GRAVITY 1800
#define DRAG 1900
#define GRAVITYREMOVEMAXTIME 0.25

@implementation EnemyInspector
static NSString * ImgChars = @"chars_50x80.png";
int counter;




+ (id) enemyInspectorWithOrigin:(CGPoint)Origin index:(int)Index
{
	return [[[self alloc] initWithOrigin:Origin index:Index] autorelease];
}

- (id) initWithOrigin:(CGPoint)Origin index:(int)Index
{
	if ((self = [super initWithX:Origin.x y:Origin.y graphic:nil])) {
        [self loadGraphicWithParam1:ImgChars param2:YES param3:NO param4:50 param5:80];
        
        self.width = 30;
        self.height = 41;
        self.offset = CGPointMake(10, 39);
        
        self.health = ORIGINAL_HEALTH;
        self.acceleration = CGPointMake(0, GRAVITY);
        self.drag = CGPointMake(0, DRAG);
        index = Index;
		originalHealth = ORIGINAL_HEALTH;
        
        self.gravityRemovalTimer = 0.0;
        self.gravityRemovalMaxTimeAllowed = GRAVITYREMOVEMAXTIME;
        maxVelocity.x = 300;
        maxVelocity.y = 430;
        
        [self addAnimationWithParam1:@"idle" param2:[NSMutableArray intArrayWithSize:1 ints:3] param3:0];
        [self addAnimationWithParam1:@"talk" param2:[NSMutableArray intArrayWithSize:2 ints:3,56] param3:8];       
        [self addAnimationWithParam1:@"walk" param2:[NSMutableArray intArrayWithSize:6 ints:24,25,26,27,28,29] param3:16];
        [self addAnimationWithParam1:@"run" param2:[NSMutableArray intArrayWithSize:6 ints:24,25,26,27,28,29] param3:24];
        [self addAnimationWithParam1:@"runPreview" param2:[NSMutableArray intArrayWithSize:6 ints:24,25,26,27,28,29] param3:16];
        [self addAnimationWithParam1:@"death" param2:[NSMutableArray intArrayWithSize:8 ints:96,97,98,99,100,100,100,100] param3:12 param4:NO];
        
        [self play:@"walk"];
        self.originalXPos = Origin.x;
        self.originalYPos = Origin.y;
        
	}
	
	return self;	
}


- (void) dealloc
{
	[super dealloc];
}

//- (void) hurt:(float)Damage
//{
//    //NSLog(@"health of hurt enemy: %f", self.health);
//    [super hurt:Damage];
//    
//    
//}

- (void) render
{
    [super render];
}


- (void) update
{   
    if (self.acceleration.y == 0)
    {
        self.gravityRemovalTimer += FlxG.elapsed;
    }
    if (self.gravityRemovalTimer > self.gravityRemovalMaxTimeAllowed)
    {
        self.gravityRemovalTimer=0.0;
        self.acceleration = CGPointMake(0, GRAVITY);
        self.drag = CGPointMake(0, DRAG);
        
    }
    
    [super update];
    
	
}
- (void) killAddStat
{
    NSString * keyV = [NSString stringWithFormat:@"%@_level_insp%i", FlxG.shared.levelLocation, FlxG.level];
    [FlxG.shared setValueForStoredDefault:@"1" forKey:keyV];
}

@end
