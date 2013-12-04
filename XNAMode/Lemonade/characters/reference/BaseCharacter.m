#import "BaseCharacter.h"

#define MOVE_SPEED_ON_SUGAR_HIGH 235;
#define JUMP_HEIGHT_ON_SUGAR_HIGH 0.250;
#define MOVE_SPEED_REGULAR 200;
#define JUMP_HEIGHT_REGULAR 0.170;


static NSString * ImgPlayer = @"chars_50x80.png";

@implementation BaseCharacter

@synthesize isPiggyBacking;

@synthesize jump;
@synthesize jumpLimit;
@synthesize ableToStartJump;
@synthesize isPlayerControlled;
@synthesize isAirDashing;
@synthesize dontDash;

@synthesize dying;
@synthesize readyToSwitchCharacters;
@synthesize jumpInitialMultiplier;
@synthesize jumpInitialTime;
@synthesize jumpSecondaryMultiplier;
@synthesize jumpTimer;
@synthesize cutSceneMode;

@synthesize startsFirst;
@synthesize levelName;
@synthesize andreInitialFlip;
@synthesize liselotInitialFlip;

@synthesize jumpCounter;
@synthesize airDashTimer;
@synthesize moveSpeed;
@synthesize timeOnLeftArrow;
@synthesize timeOnRightArrow;


@synthesize canDoubleJump;


@synthesize ability_AirDash;
@synthesize ability_DoubleJump;


@synthesize _canJump;
@synthesize hasDoubleJumped;

@synthesize followWidth;
@synthesize followHeight;

@synthesize levelStartX;
@synthesize levelStartY;
@synthesize isMale;



+ (id) baseCharacterWithOrigin:(CGPoint)Origin
{
	return [[[self alloc] initWithOrigin:Origin] autorelease];
}

- (id) initWithOrigin:(CGPoint)Origin
{
	if ((self = [super initWithX:Origin.x y:Origin.y graphic:nil])) {
        
        [self loadGraphicWithParam1:ImgPlayer param2:YES param3:NO param4:50 param5:80];
        
        self.isPlayerControlled=YES;
        
		//gravity
		self.acceleration = CGPointMake(0, 1800);
        self.gravityRemovalTimer = 0.0;
        self.gravityRemovalMaxTimeAllowed = 2.0;
        self.gravityRemovalMultiplier = 1.0;
		
		self.drag = CGPointMake(1900, 1900);
        
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
        
        maxVelocity.x = 300;
        maxVelocity.y = 430;
        
        onFloor=YES;
        
        cutSceneMode=NO;
        
        jumpCounter=0;
        
        //setting to a large number means air dash won't happen on level start.
        airDashTimer=11110;
        
        canDoubleJump=NO;
        
        ability_AirDash = YES;
        ability_DoubleJump = YES;
        
        _canJump=NO;
        
        hasDoubleJumped=NO;
        
        dontDash=YES;
        
        [self addAnimationWithParam1:@"piggyback_run" param2:[NSMutableArray intArrayWithSize:6 ints:72,73,74,75,76,77] param3:12 param4:YES];
        [self addAnimationWithParam1:@"piggyback_idle" param2:[NSMutableArray intArrayWithSize:1 ints:78] param3:0];
        [self addAnimationWithParam1:@"piggyback_jump" param2:[NSMutableArray intArrayWithSize:3 ints:76,77,76] param3:4 param4:YES];
        [self addAnimationWithParam1:@"piggyback_dash" param2:[NSMutableArray intArrayWithSize:1 ints:80] param3:0];
        
        
        
        
        [self addAnimationWithParam1:@"run" param2:[NSMutableArray intArrayWithSize:6 ints:10,11,6,7,8,9] param3:16];
        [self addAnimationWithParam1:@"run_push_crate" param2:[NSMutableArray intArrayWithSize:6 ints:46,47,42,43,44,45] param3:16 param4:YES];
        [self addAnimationWithParam1:@"dash" param2:[NSMutableArray intArrayWithSize:1 ints:79] param3:0];
        [self addAnimationWithParam1:@"idle" param2:[NSMutableArray intArrayWithSize:1 ints:51] param3:0];
        [self addAnimationWithParam1:@"talk" param2:[NSMutableArray intArrayWithSize:6 ints:51,48,51,49,51,50] param3:12];
        [self addAnimationWithParam1:@"jump" param2:[NSMutableArray intArrayWithSize:3 ints:46,47,46] param3:4 param4:YES];
        [self addAnimationWithParam1:@"death" param2:[NSMutableArray intArrayWithSize:8 ints:60,60,61,61,62,62,63,63] param3:12 param4:NO];
        
        [self play:@"idle"];
		
	}
	
	return self;
}


- (void) dealloc
{
	
	[super dealloc];
}

#pragma mark -
#pragma mark Hits

- (void) hitBottomWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    if ((FlxG.touches.virtualPadButton2 || FlxG.touches.iCadeA) && jumpCounter>0) {
        ableToStartJump = NO;
    }
    
    else {
        ableToStartJump=YES;
        jumpCounter=0;
        
    }
    
    
    jump = 0;
    
    canDoubleJump=NO;
    hasDoubleJumped=NO;
        
    [super hitBottomWithParam1:Contact param2:Velocity];
    
    if ([Contact isKindOfClass:[FlxSpriteOnPath class]]) {
        
    }

    else if (self.velocity.y<1) {
        self.y = roundf(self.y);
    }
    
}

- (void) hitTopWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    ////MWLogDebug(@"HITTING TOP, dead? %d", self.dead);
    
    if ([Contact isKindOfClass:[FlxSpriteOnPath class]] && self.onFloor) {
        self.dead = YES;
    }
    else if (self.dead)
    {
        return;
    }
    
    [super hitTopWithParam1:Contact param2:Velocity];
    
}
- (void) hitRightWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    moveSpeed = 15;
    
    self.velocity = CGPointMake(self.velocity.x * -1, self.velocity.y);
        
    [super hitRightWithParam1:Contact param2:Velocity];
    
}
- (void) hitLeftWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    moveSpeed = 15;
    
    self.velocity = CGPointMake(self.velocity.x * -1, self.velocity.y);

    
    [super hitLeftWithParam1:Contact param2:Velocity];
    
}

- (void) stopDash
{
    airDashTimer=1000000;
}



- (void) update
{
    
//    if (FlxG.touches.touchesBegan &&
//        FlxG.touches.touchBeganPoint.x>self.x &&
//        FlxG.touches.touchBeganPoint.x<self.x+self.width &&
//        FlxG.touches.touchBeganPoint.y>self.y &&
//        FlxG.touches.touchBeganPoint.y<self.y+self.height)
//    {
//        self.isPlayerControlled = ! self.isPlayerControlled;
//        
//    }
    
    
    
    
//    if ([self onScreenWithCustomXOffset:5 withCustomYOffset:5])
//    {
//        self.isPlayerControlled=YES;
//    }
//    else
//    {
//        self.isPlayerControlled=NO;
//        
//    }
//    
//    if (self.isPlayerControlled==NO)
//    {
//        self.alpha = 0.25;
//    }
//    else {
//        self.alpha = 1;
//    }
    


    

    airDashTimer+=FlxG.elapsed;
    
    if (onLeft || onRight){
        moveSpeed=115;
    }
    else {
        moveSpeed+=40;
        if (moveSpeed>maxVelocity.x) moveSpeed=maxVelocity.x;
    }
    if (FlxG.touches.virtualPadLeftArrow || FlxG.touches.iCadeLeft) {
        timeOnLeftArrow+=FlxG.elapsed;
    }
    else {
        timeOnLeftArrow=0;
    }
    if (FlxG.touches.virtualPadRightArrow || FlxG.touches.iCadeRight) {
        timeOnRightArrow+=FlxG.elapsed;
    }
    else {
        timeOnRightArrow=0;
    }
    
    self.isAirDashing=NO;
    
    if (self.isPlayerControlled ) {
        
        if (airDashTimer>0.076) {
            if ((FlxG.touches.virtualPadLeftArrow || FlxG.touches.iCadeLeft) && !self.dead) {
                
                if (timeOnLeftArrow<0.25){
                    self.velocity = CGPointMake(-(150 + (timeOnLeftArrow*200)), self.velocity.y);
                }
                else {
                    self.velocity = CGPointMake(-moveSpeed, self.velocity.y);
                    
                }
                self.scale = CGPointMake(-1, 1);
            }
            if ((FlxG.touches.virtualPadRightArrow || FlxG.touches.iCadeRight) && !self.dead) {
                if (timeOnRightArrow<0.25){
                    self.velocity = CGPointMake(150 + (timeOnRightArrow*200), self.velocity.y);
                }
                else {
                    self.velocity = CGPointMake(moveSpeed, self.velocity.y);
                    
                }
                self.scale = CGPointMake(1, 1);
            }
            
            
            //jumping Mario Style
            if(jump >= 0 && (FlxG.touches.virtualPadButton2 || FlxG.touches.iCadeA) && !self.dead && ableToStartJump)
            {
                // first press of jump
                if (jump==0) {
                    jumpCounter++;
                    [FlxG playWithParam1:SFX_JUMP_ANDRE param2:0.5];
                    
                    
                }
                
                jump += FlxG.elapsed;
                if(jump > jumpTimer) jump = -1; //You can't jump for more than the jump timer
            }
            else jump = -1;
            
            if (jump > 0)
            {
                if(jump < jumpInitialTime)
                    velocity.y = -maxVelocity.y*jumpInitialMultiplier; //This is the minimum speed of the jump
                else
                    velocity.y = -maxVelocity.y*jumpSecondaryMultiplier; //The general acceleration of the jump
            }
        }
        else {
            if (self.scale.x > 0  ) { //FlxG.touches.swipedUp ||
                self.velocity = CGPointMake(600, self.velocity.y);
                self.isAirDashing=YES;
            } else if (self.scale.x < 0 ) { //FlxG.touches.swipedDown  ||
                self.velocity = CGPointMake(-600, self.velocity.y);
                self.isAirDashing=YES;
            }
        }
        
        //jump a second time
        
        if ((!FlxG.touches.virtualPadButton2 && !FlxG.touches.iCadeA) && !self.onFloor) {
            canDoubleJump=YES;
        }
        
        
        
        //air dash ----  && [[FlxG following] isKindOfClass:[self class]] if you only want to dash while following.
        if (self.ability_AirDash && !self.dead) {
            if (self.scale.x > 0 && ((FlxG.touches.virtualPadButton1 && FlxG.touches.newTouch) || FlxG.touches.iCadeBBegan) && !self.dead && self.dontDash) { //FlxG.touches.swipedUp ||
                self.velocity = CGPointMake(600, self.velocity.y);
                airDashTimer=0;
                self.isAirDashing=YES;
                [FlxG playWithParam1:SFX_WHOOSH param2:0.1 param3:NO];
                
                
            }
            
            else if (self.scale.x < 0 && ((FlxG.touches.virtualPadButton1 && FlxG.touches.newTouch) || FlxG.touches.iCadeBBegan)&& !self.dead && self.dontDash)
            { //FlxG.touches.swipedDown  ||
                self.velocity = CGPointMake(-600, self.velocity.y);
                airDashTimer=0;
                self.isAirDashing=YES;
                
                [FlxG playWithParam1:SFX_WHOOSH param2:0.1 param3:NO];
                
                
            }
        }
        
        if (self.ability_DoubleJump) {

            if ( (FlxG.touches.virtualPadButton2 || FlxG.touches.iCadeA) && canDoubleJump && !hasDoubleJumped && jumpCounter==1  && !self.onFloor) {
                
                self.velocity = CGPointMake(self.velocity.x, -410);
                jumpCounter++;
                [FlxG play:SFX_JUMP_ANDRE withVolume:0.25];
                [FlxG playWithParam1:SFX_JUMP_ANDRE param2:0.5];
                

                canDoubleJump=NO;
                hasDoubleJumped=YES;
                
            }
        }
        
    }
    
    
    if (self.scale.x > 0) {
        if (self.velocity.x > 1 && onFloor) {
            if (onRight || onLeft) {
                if (self.isPiggyBacking) [self play:@"piggyback_run"];
                else [self play:@"run_push_crate"];
            }
            else {
                if (self.isAirDashing) {
                    if (self.isPiggyBacking) [self play:@"piggyback_dash"];
                    else [self play:@"dash"];
                }
                
                else {
                    if (self.isPiggyBacking) [self play:@"piggyback_run"];
                    else [self play:@"run"];
                    
                }
            }
        }
        else if (dying || dead) {
            [self play:@"death"];
            
        }
        else if (!onFloor) {
            if (self.isAirDashing)
                if (self.isPiggyBacking) [self play:@"piggyback_dash"];
                else [self play:@"dash"];
            else {
                if (self.acceleration.y == 0) {
                    if (self.isPiggyBacking) [self play:@"piggyback_jump"];
                    else [self play:@"fly"];                }
                else {
                    if (self.isPiggyBacking) [self play:@"piggyback_jump"];
                    else [self play:@"jump"];
                }
            }
        }
        else {
            if (self.isPiggyBacking) [self play:@"piggyback_idle"];
            else [self play:@"idle"];
        }
    }
    
    //facing left.
    
    if (self.scale.x < 0) {
        if (self.velocity.x < -1 && onFloor) {
            if (onRight || onLeft) {
                if (self.isPiggyBacking) [self play:@"piggyback_run"];
                else [self play:@"run_push_crate"];
            }
            else {
                if (self.isAirDashing) {
                    if (self.isPiggyBacking) [self play:@"piggyback_dash"];
                    else [self play:@"dash"];
                }
                
                else {
                    if (self.isPiggyBacking) [self play:@"piggyback_run"];
                    else [self play:@"run"];
                    
                }
            }
        }
        else if (dying || dead) {
            [self play:@"death"];
        }
        else if (!onFloor) {
            if (self.isAirDashing)
                if (self.isPiggyBacking) [self play:@"piggyback_dash"];
                else [self play:@"dash"];
                else {
                    if (self.acceleration.y == 0) {
                        if (self.isPiggyBacking) [self play:@"piggyback_jump"];
                        else [self play:@"fly"];                }
                    else {
                        if (self.isPiggyBacking) [self play:@"piggyback_jump"];
                        else [self play:@"jump"];
                    }
                }
        }
        else {
            if (self.isPiggyBacking) [self play:@"piggyback_idle"];
            else [self play:@"idle"];
        }
    }
    
    
    
    // Wrap around levels.
    
    if (self.y > FlxG.levelHeight) {
        self.y = 0;
    }
//    if (self.y < 0) {
//        self.y = FlxG.levelHeight;
//    }
    if (self.x > FlxG.levelWidth) {
        self.x = 0 ;
    }
    if (self.x < 0 ) {
        self.x = FlxG.levelWidth;
    }
    
    
    // squashed by moving boxes
    
//    if (self.onFloor && self.onTop)
//    {
//        self.dead = YES;
//        
//    }
    
    ////MWLogDebug(@"%u", _curFrame);

	
	[super update];
    
    if (_curFrame==7) {
        //dying = NO;
        readyToSwitchCharacters=YES;
        
    }
    
    
    dontDash=YES;
    
}


@end
