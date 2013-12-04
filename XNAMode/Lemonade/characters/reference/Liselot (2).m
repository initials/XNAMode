#import "Liselot.h"

#import "Andre.h"

#import "MrAmsterdaam.h"
#import "Inspector.h"
#import "WorkingManHero.h"
#import "Chef.h"

#import "EnemyArmy.h"
#import "EnemyChef.h"
#import "EnemyInspector.h"
#import "EnemyWorker.h"

#define GRAVITY 1800
#define DRAG 1900
#define GRAVITYREMOVEMAXTIME 0.345

@implementation Liselot


+ (id) liselotWithOrigin:(CGPoint)Origin
{
	return [[[self alloc] initWithOrigin:Origin ] autorelease];
}

- (id) initWithOrigin:(CGPoint)Origin
{
	if ((self = [super initWithX:Origin.x y:Origin.y graphic:nil])) {
        
        [self loadGraphicWithParam1:@"chars_50x80.png" param2:YES param3:NO param4:50 param5:80];
        
        self.isPlayerControlled=YES;
        
        self.isPiggyBacking = NO;
        
        
		//gravity
		self.acceleration = CGPointMake(0, GRAVITY);
        self.gravityRemovalTimer = 0.0;
        self.gravityRemovalMaxTimeAllowed = GRAVITYREMOVEMAXTIME;
        self.gravityRemovalMultiplier = 1.0;

        
		self.drag = CGPointMake(DRAG, DRAG);
        self.width = 10;
        self.height = 41;
        self.offset = CGPointMake(20, 39);
        self.originalXPos = Origin.x;
        self.originalYPos = Origin.y;
        
        self.levelStartX = Origin.x;
        self.levelStartY = Origin.y;
        
        self.followWidth=0;
        self.followHeight=0;
        
        //jumping variables
        
        jump=0;
        jumpCounter=0;
        jumpInitialMultiplier=0.64;   // 0.550
        jumpSecondaryMultiplier=.68;  //1.0
        jumpInitialTime= 0.248;        // 0.095
        jumpTimer=0.34;             //0.170
        
        moveSpeed=200;
        timeOnLeftArrow=0;
        timeOnRightArrow=0;
        
        readyToSwitchCharacters=NO;
        dying=NO;
        ableToStartJump=YES;
        
        maxVelocity.x = 310;
        maxVelocity.y = 430;
        
        onFloor=YES;
        
        cutSceneMode=NO;
        
        jumpCounter=0;
        
        //setting to a large number means air dash won't happen on level start.
        airDashTimer=11110;
        
        
        
        
        canDoubleJump=NO;
        
        
        
        

        
        
        _canJump=NO;
        
        hasDoubleJumped=NO;
        
        dontDash=YES;
        
        [self addAnimationWithParam1:@"piggyback_run" param2:[NSMutableArray intArrayWithSize:6 ints:72,73,74,75,76,77] param3:12 param4:YES];
        [self addAnimationWithParam1:@"piggyback_idle" param2:[NSMutableArray intArrayWithSize:1 ints:78] param3:0];
        [self addAnimationWithParam1:@"piggyback_jump" param2:[NSMutableArray intArrayWithSize:3 ints:76,77,76] param3:4 param4:YES];
        [self addAnimationWithParam1:@"piggyback_dash" param2:[NSMutableArray intArrayWithSize:1 ints:80] param3:0];
       
        
        
        
        [self addAnimationWithParam1:@"run" param2:[NSMutableArray intArrayWithSize:6 ints:12,13,14,15,16,17] param3:16];
        [self addAnimationWithParam1:@"run_push_crate" param2:[NSMutableArray intArrayWithSize:6 ints:69,70,71,81,82,83] param3:16 param4:YES];
        [self addAnimationWithParam1:@"idle" param2:[NSMutableArray intArrayWithSize:1 ints:2] param3:0];
        [self addAnimationWithParam1:@"talk" param2:[NSMutableArray intArrayWithSize:6 ints:2,55] param3:12];
        [self addAnimationWithParam1:@"jump" param2:[NSMutableArray intArrayWithSize:3 ints:15,16,17] param3:4 param4:YES];
        [self addAnimationWithParam1:@"death" param2:[NSMutableArray intArrayWithSize:8 ints:64,64,65,65,66,66,67,67] param3:12 param4:NO];
        
        [self play:@"idle"];
        
        ability_AirDash = NO;
        ability_DoubleJump = YES;
        
        
        
	}
	
	return self;
}


- (void) dealloc
{
	//[speechBubble release];
	[super dealloc];
}


- (void) hitBottomWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
        
    [super hitBottomWithParam1:Contact param2:Velocity];
    
    
    if ([Contact isKindOfClass:[MrAmsterdaam class]] ||
        [Contact isKindOfClass:[Inspector class]] ||
        [Contact isKindOfClass:[WorkingManHero class]] ||
        [Contact isKindOfClass:[Chef class]] ||
        [Contact isKindOfClass:[EnemyArmy class]] ||
        [Contact isKindOfClass:[EnemyChef class]] ||
        [Contact isKindOfClass:[EnemyInspector class]] ||
        [Contact isKindOfClass:[EnemyWorker class]]
        
        ) {
        
        Contact.dead = YES;
                
        self.velocity = CGPointMake(self.velocity.x, -2200/5);
        self.acceleration = CGPointMake(0, 0);
        self.drag = CGPointMake(0, 0);
        self.gravityRemovalMultiplier = 2.5;

        
        [FlxG playWithParam1:SFX_JUMP_LISELOT param2:0.5];

        
    }
    else {
        self.gravityRemovalMultiplier = 1.0;
    }
    
    
}

- (void) hitTopWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    
    [super hitTopWithParam1:Contact param2:Velocity];
    
}
- (void) hitRightWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    
    [super hitRightWithParam1:Contact param2:Velocity];
    if ([Contact isKindOfClass:[MrAmsterdaam class]] ||
        [Contact isKindOfClass:[Inspector class]] ||
        [Contact isKindOfClass:[WorkingManHero class]] ||
        [Contact isKindOfClass:[Chef class]] ||
        [Contact isKindOfClass:[EnemyArmy class]] ||
        [Contact isKindOfClass:[EnemyChef class]] ||
        [Contact isKindOfClass:[EnemyInspector class]] ||
        [Contact isKindOfClass:[EnemyWorker class]]
        
        )
    {
        self.dead = YES;
        self.isPiggyBacking = NO;
    }
}
- (void) hitLeftWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    
    [super hitLeftWithParam1:Contact param2:Velocity];
    if ([Contact isKindOfClass:[MrAmsterdaam class]] ||
        [Contact isKindOfClass:[Inspector class]] ||
        [Contact isKindOfClass:[WorkingManHero class]] ||
        [Contact isKindOfClass:[Chef class]] ||
        [Contact isKindOfClass:[EnemyArmy class]] ||
        [Contact isKindOfClass:[EnemyChef class]] ||
        [Contact isKindOfClass:[EnemyInspector class]] ||
        [Contact isKindOfClass:[EnemyWorker class]]
        
        )
    {
        self.dead = YES;
        self.isPiggyBacking = NO;
    }
}
- (void) bounceOffEnemy
{
    self.velocity = CGPointMake(self.velocity.x, -1800/5);
    self.acceleration = CGPointMake(0, 0);
    self.drag = CGPointMake(0, 0);
    self.gravityRemovalMultiplier = 2.5;
    
    
    [FlxG playWithParam1:SFX_BOUNCE_LISELOT param2:0.6];
    
    
    
}


- (void) update
{
    if ([self._curAnim.name isEqualToString:@"death"] && _curFrame==0)
    {
        [FlxG playWithParam1:SFX_DEATH param2:0.85];
    }
    if ([self._curAnim.name isEqualToString:@"death"] && _curFrame==7)
    {
        self.x = self.originalXPos;
        self.y = self.originalYPos;
        
        self.dead = NO;
        
        FlxG.shared.livesLiselot --;
        
        
        //MWLogDebug(@"Liselot has died. Lives are now: %d", FlxG.shared.livesLiselot);
        
        
        
    }
    
    
    
    
    if (self.acceleration.y == 0)
    {
        self.gravityRemovalTimer += FlxG.elapsed * self.gravityRemovalMultiplier;
    }
    if (self.gravityRemovalTimer > self.gravityRemovalMaxTimeAllowed)
    {
        self.gravityRemovalTimer=0.0;
        self.acceleration = CGPointMake(0, GRAVITY);
        self.drag = CGPointMake(DRAG, DRAG);
        
    }
    
	[super update];
    
}


@end
