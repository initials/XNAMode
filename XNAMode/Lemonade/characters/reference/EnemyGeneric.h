//
//  Enemy.h
//  FlxShmup
//
//  Created by Shane Brouwer on 7/04/11.
//  Copyright 2011 Initials. All rights reserved.
//
#import "Enemy.h"

@interface EnemyGeneric : FlxSprite
{
    //NSArray * animationList;
    int _index;
    
}

+ (id) enemyGenericWithOrigin:(CGPoint)Origin index:(int)Index;
- (id) initWithOrigin:(CGPoint)Origin index:(int)Index;

@property int _index;



@end
