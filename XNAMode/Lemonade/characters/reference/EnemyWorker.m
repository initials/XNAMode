
#import "EnemyWorker.h"

#define ORIGINAL_HEALTH 10
#define GRAVITY 1800
#define DRAG 1900
#define GRAVITYREMOVEMAXTIME 0.25

@implementation EnemyWorker
static NSString * ImgChars = @"chars_50x80.png";
int counter;




+ (id) enemyWorkerWithOrigin:(CGPoint)Origin index:(int)Index
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
        
        self.gravityRemovalTimer = 0.0;
        self.gravityRemovalMaxTimeAllowed = GRAVITYREMOVEMAXTIME;
        
        self.x = Origin.x;
        self.y = Origin.y;
        maxVelocity.x = 300;
        maxVelocity.y = 430;
        
        
        index = Index;
		originalHealth = ORIGINAL_HEALTH;
        [self addAnimationWithParam1:@"idle" param2:[NSMutableArray intArrayWithSize:1 ints:0] param3:0];
        [self addAnimationWithParam1:@"talk" param2:[NSMutableArray intArrayWithSize:4 ints:0, 53, 52, 54] param3:6];  
        [self addAnimationWithParam1:@"walk" param2:[NSMutableArray intArrayWithSize:6 ints:18,19,20,21,22,23] param3:16];
        [self addAnimationWithParam1:@"run" param2:[NSMutableArray intArrayWithSize:6 ints:18,19,20,21,22,23] param3:32];
        [self addAnimationWithParam1:@"runPreview" param2:[NSMutableArray intArrayWithSize:6 ints:18,19,20,21,22,23] param3:16];
        [self addAnimationWithParam1:@"death" param2:[NSMutableArray intArrayWithSize:8 ints:91,92,93,94,95,95,95,95] param3:12 param4:NO];
        [self play:@"walk"];
        self.originalXPos = Origin.x;
        self.originalYPos = Origin.y;
        
	}
	
	return self;	
}




- (void) render
{
    [super render];
}


- (void) dealloc
{
	[super dealloc];
}


- (void) update
{
    
   //NSLog(@"y value of worker : %f", self.velocity.x);
    
    
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
    NSString * keyV = [NSString stringWithFormat:@"%@_level_work%i", FlxG.shared.levelLocation, FlxG.level];
    [FlxG.shared setValueForStoredDefault:@"1" forKey:keyV];
}

@end
