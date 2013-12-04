
#import "EnemyArmy.h"

#define ORIGINAL_HEALTH 10

#define GRAVITY 1800
#define DRAG 1900
#define GRAVITYREMOVEMAXTIME 0.25

@implementation EnemyArmy
static NSString * ImgChars = @"chars_50x80.png";
int counter;



+ (id) enemyArmyWithOrigin:(CGPoint)Origin index:(int)Index
{
	return [[[self alloc] initWithOrigin:Origin index:Index] autorelease];
}

- (id) initWithOrigin:(CGPoint)Origin index:(int)Index
{
	if ((self = [super initWithX:Origin.x y:Origin.y graphic:nil])) {
                
        [self loadGraphicWithParam1:ImgChars param2:YES param3:NO param4:50 param5:80];
                
        self.width = 30;
        self.height = 40;
        self.offset = CGPointMake(10, 40);
        
        self.health = ORIGINAL_HEALTH;
        self.acceleration = CGPointMake(0, GRAVITY);
        self.drag = CGPointMake(0, DRAG);
        
        self.gravityRemovalTimer = 0.0;
        self.gravityRemovalMaxTimeAllowed = GRAVITYREMOVEMAXTIME;
        maxVelocity.x = 300;
        maxVelocity.y = 500;
        
        index = Index;
		originalHealth = ORIGINAL_HEALTH;
        [self addAnimationWithParam1:@"idle" param2:[NSMutableArray intArrayWithSize:1 ints:4] param3:0];
        [self addAnimationWithParam1:@"walk" param2:[NSMutableArray intArrayWithSize:6 ints:36,37,38,39,40,41] param3:16];
        [self addAnimationWithParam1:@"run" param2:[NSMutableArray intArrayWithSize:6 ints:36,37,38,39,40,41] param3:24];
        [self addAnimationWithParam1:@"runPreview" param2:[NSMutableArray intArrayWithSize:6 ints:36,37,38,39,40,41] param3:16];
        [self addAnimationWithParam1:@"talk" param2:[NSMutableArray intArrayWithSize:2 ints:4,57] param3:6];       
        [self addAnimationWithParam1:@"death" param2:[NSMutableArray intArrayWithSize:8 ints:84,85,86,87,88,89,90,88] param3:12 param4:NO];

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


//- (void) hitLeftWithParam1:(FlxObject *)Contact param2:(float)Velocity
//{
//    [super hitLeftWithParam1:Contact param2:Velocity];
//}
//
//- (void) hitRightWithParam1:(FlxObject *)Contact param2:(float)Velocity
//{    
//    [super hitRightWithParam1:Contact param2:Velocity];
//}



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
    
    
//    if (self.x >= limitX ) {
//        self.x = limitX-1;
//        CGFloat i = self.velocity.x * -1;
//        self.velocity = CGPointMake(i, 0);
//    } else if (self.x <= originalXPos) {
//        self.x = originalXPos+1;
//        CGFloat i = self.velocity.x * -1;
//        self.velocity = CGPointMake(i, 0);        
//    }
    
    //if touches 
    if (FlxG.touches.touchesBegan &&
        FlxG.touches.touchBeganPoint.x>self.x &&
        FlxG.touches.touchBeganPoint.x<self.x+self.width &&
        FlxG.touches.touchBeganPoint.y>self.y &&
        FlxG.touches.touchBeganPoint.y<self.y+self.height) 
    {
        //NSLog(@" A STORY " );
        
    }
    
    

    
    [super update];
    
	
}

- (void) killAddStat
{
    NSString * keyV = [NSString stringWithFormat:@"%@_level_army%i", FlxG.shared.levelLocation, FlxG.level];
    [FlxG.shared setValueForStoredDefault:@"1" forKey:keyV];
}

@end
