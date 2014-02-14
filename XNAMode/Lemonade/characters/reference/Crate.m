#import "Crate.h"
#import "Andre.h"
#import "Liselot.h"
#import "MrAmsterdaam.h"
#import "Chef.h"
#import "Inspector.h"
#import "WorkingManHero.h"


static NSString * ImgCrate = @"smallCrateExplode.png";
static NSString * SndExp = @"grenadeExplosion.caf";

@implementation Crate

@synthesize canBePushedRight;
@synthesize canBePushedLeft;
@synthesize canBeReset;
@synthesize lastDirectionPushed;

float originalXPos;
float originalYPos;

//FlxSprite * resetText;

+ (id) crateWithOrigin:(CGPoint)Origin
{
	return [[[self alloc] initWithOrigin:Origin] autorelease];
}

- (id) initWithOrigin:(CGPoint)Origin
{
	if ((self = [super initWithX:Origin.x y:Origin.y graphic:nil])) {
		
		[self loadGraphicWithParam1:ImgCrate param2:YES param3:NO param4:32 param5:32];
        
        self.acceleration = CGPointMake(0, 1100);
        self.width = 30;
        self.height = 23;
        self.offset = CGPointMake(1, 9);

        self.maxVelocity = CGPointMake(200, 700);
        [self addAnimationWithParam1:@"blink" param2:[NSMutableArray intArrayWithSize:2 ints:0,1] param3:2];
        [self addAnimationWithParam1:@"explode" param2:[NSMutableArray intArrayWithSize:6 ints:2,3,4,5,6,7] param3:12];
        [self addAnimationWithParam1:@"reset" param2:[NSMutableArray intArrayWithSize:1 ints:8] param3:0];

        self.fixed=NO;
        
        [self play:@"blink"];
        self.drag = CGPointMake(340, 340);
        originalXPos=Origin.x;
        originalYPos=Origin.y;
        canBeReset=NO;   
        lastDirectionPushed=YES;
        
        
	}
	
	return self;	
}

- (void) resetPosition
{
//    if (canBeReset) {
        
        [self play:@"blink"];
        self.x = originalXPos ;
        self.y = originalYPos ;
//        canBeReset=NO;
//        resetText.alpha = 0;
//    
//    }
//    else {
//        
//        resetText.alpha = 1;
//        canBeReset=YES;
//
//    }    
}


- (void) dealloc
{
    //[resetText release];
	[super dealloc];
}

- (void) hitBottomWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    if ([Contact isKindOfClass:[Liselot class]] || 
        [Contact isKindOfClass:[Andre class]]  ||
        [Contact isKindOfClass:[Chef class]]  ||
        [Contact isKindOfClass:[MrAmsterdaam class]]  ||
        [Contact isKindOfClass:[Inspector class]] ||
        [Contact isKindOfClass:[WorkingManHero class]]
        
        ) 
    {
        
        if (lastDirectionPushed) {
            self.velocity=CGPointMake(130, 0);
        }
        else if (!lastDirectionPushed) {
            self.velocity=CGPointMake(-130, 0);
        }        
    
        


    }
    else if ([Contact isKindOfClass:[FlxSpriteOnPath class]] ){
        // DO NOTHING

    }
    else {
        
        // DO ACTUAL COLLIDE
        
        [super hitBottomWithParam1:Contact param2:Velocity];

    }
    self.y = roundf(self.y);
    
}
- (void) hitTopWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    
    [super hitTopWithParam1:Contact param2:Velocity];
    
}
- (void) hitRightWithParam1:(FlxObject *)Contact param2:(float)Velocity
{    
    lastDirectionPushed=NO;
    
    if ([Contact isKindOfClass:[Andre class]]) {
        canBePushedLeft=YES;
    }
//    if (
//        [Contact isKindOfClass:[EnemyChef class]]  || 
//        [Contact isKindOfClass:[EnemyArmy class]]  || 
//        [Contact isKindOfClass:[EnemyInspector class]] || 
//        [Contact isKindOfClass:[EnemyWorker class]]   
//        ) {
//        
//        self.fixed = YES;
//        
//    }
    

    
    [super hitRightWithParam1:Contact param2:Velocity];

}
- (void) hitLeftWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    lastDirectionPushed=YES;

    if ([Contact isKindOfClass:[Andre class]]) {
        canBePushedRight=YES;

    }
//    if (
//        [Contact isKindOfClass:[EnemyChef class]]  || 
//        [Contact isKindOfClass:[EnemyArmy class]]  || 
//        [Contact isKindOfClass:[EnemyInspector class]] || 
//        [Contact isKindOfClass:[EnemyWorker class]]   
//        ) {
//        
//        self.fixed = YES;
//        
//    }
    
    [super hitLeftWithParam1:Contact param2:Velocity];

}


//- (void) render
//{
//    [resetText render];
//    [super render];
//}


- (void) update
{   
    //NSLog(@" r %d l %d pr %d pl %d on floor %d", onRight, onLeft, canBePushedLeft, canBePushedRight, onFloor);
    
    if (self.onFloor && abs(self.velocity.x)<1) {
        self.fixed=YES;
        self.velocity=CGPointMake(0, 0);
        self.acceleration=CGPointMake(0, 0);
    }
    
    if (onRight || onLeft) {
        self.fixed=NO;
        self.acceleration=CGPointMake(0, 1100);
    }
    
    // crates will collapse and reset upon touching them.
    if (FlxG.touches.touchesBegan &&
        FlxG.touches.touchBeganPoint.x+15>self.x &&
        FlxG.touches.touchBeganPoint.x-15<self.x+self.width &&
        FlxG.touches.touchBeganPoint.y+15>self.y &&
        FlxG.touches.touchBeganPoint.y-15<self.y+self.height) 
    {
        if (canBeReset) {
            [self play:@"explode"];
            
            [FlxG playWithParam1:@"grenadeExplosion.caf" param2:0.35 param3:NO];
            
            //self.velocity=CGPointMake(100, 0);
            canBeReset=NO;
//            resetText.alpha = 0;
        } else {
            canBeReset=YES;
            [self play:@"reset"];
//            resetText.alpha = 1;
        }
        
    } else {
        if (FlxG.touches.touchesBegan && canBeReset) {
            [self play:@"blink"];
            canBeReset=NO;
        }
    }
    

    
	//self.velocity = CGPointMake(self.velocity.x, self.velocity.y);
//    
//    resetText.x=self.x;
//    resetText.y=self.y-10;
//    
//    [resetText update];
    

    
	[super update];
    
    //at the last frame of the implosion. reset position.
    if (_curFrame==5) {
        [self play:@"blink"];
        [self resetPosition];
        self.fixed=NO;
        self.acceleration=CGPointMake(0, 1100);
    }
    
    //if it falls off the screen reset
    if (self.y > FlxG.levelHeight) {
        [self resetPosition];
    }
    
    canBePushedRight=NO;
	canBePushedLeft=NO;
    
}


@end
